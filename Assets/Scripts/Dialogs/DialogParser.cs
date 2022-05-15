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
        NewInput input;
        Character.CharacterController player;

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
            yield break;
        }

        public IEnumerator ProcessDialogs()
        {
            endProcess = false;
            if (parsing)
                yield return new WaitUntil(() => !parsing);
            if (dialogs.Count != parsedDialogs.Count)
                yield return ParseDialogs();
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
                    break;
                }
                currDialog++;
            }
            output.transform.parent.gameObject.SetActive(false);
        }

        public IEnumerator ProcessDialog(string id) => ProcessDialog(FindDialogWithID(id));

        public IEnumerator ProcessDialog(Dialog d)
        {
            if (input is null)
                input = new NewInput();
            if (player is null)
                player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Character.CharacterController>();
            player.PauseMovement();
            input.Dialogs.Enable();
            if (d is null)
            {
                Debug.LogError("Dialog is null");
                goto exit;
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
                                            //if (!t.Content.EndsWith(" "))
                                            //    output.text += " ";
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
                                        goto exit;
                                    default:
                                        ProcessGoto(s.Goto);
                                        continue;
                                }
                            }
                        }
                        break;
                    case Choice c:
                        {
                            var choice = 0;
                            var written = false;
                        parseChoice:
                            if (!written)
                            {
                                output.text = "";
                                foreach (char ch in c.Prompt)
                                {
                                    output.text += ch;
                                    audio?.Play();
                                    yield return new WaitForSeconds((float)d.CharDelay.Chain(defaultCharDelay));
                                }
                                written = true;
                            }
                            else
                                output.text = c.Prompt;
                            for (int i = 0; i < c.Options.Count; i++)
                            {
                                var o = c.Options[i];
                                if (choice == i)
                                    output.text += $"\n\u25cf <b>{o.Text}</b>";
                                else
                                    output.text += $"\n<alpha=#00>\u25cf <alpha=#FF>{o.Text}";
                            }
                            yield return new WaitForSecondsRealtime(.25f);
                            yield return new WaitUntil(() => input.Dialogs.Accept.IsPressed() || input.Dialogs.Select.ReadValue<float>() != 0);
                            if (input.Dialogs.Accept.IsPressed())
                            {
                                var g = c.Options[choice].Goto;
                                switch (g)
                                {
                                    case "$end":
                                    case "":
                                    case null:
                                        output.text = "";
                                        goto exit;
                                    default:
                                        ProcessGoto(g);
                                        continue;
                                }
                            }
                            var sel = input.Dialogs.Select.ReadValue<float>();
                            if (sel > 0)
                                choice = choice == 0 ? c.Options.Count - 1 : choice - 1;
                            else if (sel < 0)
                                choice = choice == c.Options.Count - 1 ? 0 : choice + 1;
                            goto parseChoice;
                        }
                    default:
                        continue;
                }
                elem++;
            }
        exit:
            player.ResumeMovement();
            input.Dialogs.Disable();
            yield break;

            void ProcessGoto(string g)
            {
                var next = FindDialogElementWithID(d, g);
                if (next != null)
                    elem = d.Content.IndexOf(next);
            }
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