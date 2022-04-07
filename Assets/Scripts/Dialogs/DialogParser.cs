using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Linq;
using TMPro;
using UnityEngine;

namespace SH.Dialogs
{
    [Serializable]
    public class DialogParser
    {
        [SerializeField]
        private double defaultCharDelay = 0.1f;
        [SerializeField]
        private double defaultSentenceDelay = 0.5f;
        [SerializeField]
        private double defaultPauseDelay = 1f;
        [SerializeField]
        private bool defaultLineBreak = true;
        [SerializeField]
        private List<TextAsset> dialogs = new List<TextAsset>();
        [SerializeField]
        private TMP_Text output;
        [SerializeField]
        private AudioSource audio;

        public string StartingDialog { get; set; }

        private List<Dialog> parsedDialogs = new List<Dialog>();
        private bool parsing = false;
        private bool endProcess = false;

        public IEnumerator ParseDialogs()
        {
            output.transform.parent.gameObject.SetActive(false);
            parsing = true;
            Debug.Log("Parsing dialogs started");
            parsedDialogs.Clear();
            foreach (var asset in dialogs)
            {
                try
                {
                    parsedDialogs.Add(Dialog.Deserialize(asset.text));
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
            Debug.Log("Parsing dialogs ended, found {0} dialogs".Format(parsedDialogs.Count));
            parsing = false;
            yield return null;
        }

        public IEnumerator ProcessDialogs()
        {
            endProcess = false;
            yield return new WaitUntil(() => !parsing);
            if (output is null)
            {
                Debug.LogError("Output text is null");
                yield break;
            }
            output.transform.parent.gameObject.SetActive(true);
            output.text = "";

            int currDialog = 0;
            if (!StartingDialog.IsNullOrEmpty())
                currDialog = parsedDialogs.FindIndex(d => d.ID == StartingDialog);
            while (currDialog < parsedDialogs.Count && currDialog != -1)
            {
                Dialog d = parsedDialogs[currDialog];
                yield return ProcessDialog(d);
                if (endProcess)
                {
                    endProcess = false;
                    yield break;
                }
                currDialog++;
            }
            output.transform.parent.gameObject.SetActive(false);
        }

        public IEnumerator ProcessDialog(string id) => ProcessDialog(FindDialogWithID(id));

        public IEnumerator ProcessDialog(Dialog d)
        {
            if (d is null)
            {
                Debug.LogError("Dialog is null");
                yield break;
            }
            output.text = "";
            int elem = 0;
            while (elem >= 0 && elem < d.Content.Count)
            {
                IDialogElement dialog = d.Content[elem];
                switch (dialog)
                {
                    case Sentence s:
                        {
                            if (s.Clear.Chain(true))
                                output.text = "";
                            foreach (ISentenceElement sel in s.Content)
                            {
                                switch (sel)
                                {
                                    case Pause p:
                                        yield return new WaitForSeconds((float)p.Delay.Chain(d.PauseDelay, defaultPauseDelay));
                                        break;
                                    case Text t:
                                        {
                                            foreach (char c in t)
                                            {
                                                output.text += c;
                                                audio?.Play();
                                                yield return new WaitForSeconds((float)s.CharDelay.Chain(d.CharDelay, defaultCharDelay));
                                            }
                                            if (!t.Content.EndsWith(" "))
                                                output.text += " ";
                                        }
                                        break;
                                    case Break b:
                                        output.text += "\n";
                                        break;
                                }
                            }
                            if (s.LineBreak.Chain(d.LineBreak, defaultLineBreak))
                                output.text += "\n";
                            yield return new WaitForSeconds((float)s.SentenceDelay.Chain(d.SentenceDelay, defaultSentenceDelay));
                            if (!s.Goto.IsNullOrEmpty())
                            {
                                switch (s.Goto)
                                {
                                    case "$end":
                                        output.text = "";
                                        yield break;
                                    default:
                                        {
                                            var next = FindDialogElementWithID(d, s.Goto);
                                            if (next != null)
                                                elem = d.Content.IndexOf(next);
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    default:
                        continue;
                }
                elem++;
            }
            yield return null;
        }

        private IDialogElement FindDialogElementWithID(Dialog d, string id)
        {
            if (id.IsNullOrEmpty())
                return null;
            return d.Content.Find(e => e.ID == id);
        }

        private Dialog FindDialogWithID(string id)
        {
            return parsedDialogs.Find(d => d.ID == id);
        }
    }
}