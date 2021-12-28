using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogParser
{
    [SerializeField]
    private float defaultCharDelay = 0.1f;
    [SerializeField]
    private float defaultSentenceDelay = 0.5f;
    [SerializeField]
    private float defaultPauseDelay = 1f;
    [SerializeField]
    private bool defaultLineBreak = true;
    [SerializeField]
    private List<TextAsset> dialogs = new List<TextAsset>();
    [SerializeField]
    private GameObject output;

    public IEnumerator ProcessDialogs()
    {
        output.SetActive(true);
        var outputText = output.GetComponentInChildren<TMP_Text>();
        if (outputText == null)
        {
            Debug.LogError("Output text not found");
            yield break;
        }
        outputText.text = "";
        foreach (TextAsset dialog in dialogs)
        {
            XmlReader reader = XmlReader.Create(new StringReader(dialog.text));
            reader.ReadToFollowing("Dialog");
            Dictionary<string, string> mainattributes = new Dictionary<string, string>();
            if (reader.HasAttributes)
            {
                while (reader.MoveToNextAttribute())
                    mainattributes[reader.Name] = reader.Value;
                reader.MoveToContent();
            }
            while (reader.ReadToFollowing("Sentence"))
            {
                Dictionary<string, string> attributes = mainattributes.DeepCopy();
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                        attributes[reader.Name] = reader.Value;
                    reader.MoveToContent();
                }
                var sub = reader.ReadSubtree();
                if (sub != null)
                    while (sub.Read())
                    {
                        switch (sub.NodeType)
                        {
                            case XmlNodeType.Element:
                                Dictionary<string, string> subattributes = attributes.DeepCopy();
                                if (sub.HasAttributes)
                                {
                                    while (sub.MoveToNextAttribute())
                                        subattributes[sub.Name] = sub.Value;
                                    sub.MoveToContent();
                                }
                                switch (sub.Name)
                                {
                                    case "Pause":
                                        float pauseDelay = defaultPauseDelay;
                                        if (subattributes.ContainsKey("pauseDelay"))
                                            float.TryParse(subattributes["pauseDelay"], NumberStyles.Float, CultureInfo.InvariantCulture, out pauseDelay);
                                        yield return new WaitForSeconds(pauseDelay);
                                        break;
                                }
                                break;
                            case XmlNodeType.Text:
                                float charDelay = defaultCharDelay;
                                if (attributes.ContainsKey("charDelay"))
                                    float.TryParse(attributes["charDelay"], NumberStyles.Float, CultureInfo.InvariantCulture, out charDelay);
                                if (charDelay > 0)
                                {
                                    foreach (char c in sub.Value)
                                    {
                                        outputText.text += c;
                                        yield return new WaitForSeconds(charDelay);
                                    }
                                }
                                else
                                    outputText.text += sub.Value;
                                break;
                        }
                    }
                float sentenceDelay = defaultSentenceDelay;
                if (attributes.ContainsKey("sentenceDelay"))
                    float.TryParse(attributes["sentenceDelay"], NumberStyles.Float, CultureInfo.InvariantCulture, out sentenceDelay);
                yield return new WaitForSeconds(sentenceDelay);
                bool lineBreak = defaultLineBreak;
                if (attributes.ContainsKey("lineBreak"))
                    bool.TryParse(attributes["lineBreak"], out lineBreak);
                if (lineBreak)
                    outputText.text += "\n";
            }
            yield return new WaitForSeconds(2);
        }
    }
}
