using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using SH.Dialogs;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    [InspectorName("Dialog Settings")]
    private DialogParser dialogParser = new DialogParser();
    [SerializeField]
    private List<Interaction> interactions = new List<Interaction>();
    [SerializeField]
    private TextAsset testDialog;

    private void Awake()
    {
        StartCoroutine(dialogParser.ParseDialogs());
        StartCoroutine(dialogParser.ProcessDialogs());
        //Dialog d = new Dialog();
        //d.Content.Add(new Sentence("Test"));
        //d.Content.Add(new Sentence(new Text("Test"), new Pause(), new Text("Test cd")) { Goto = "$end" });
        //Debug.Log(d.Serialize());
        //StartCoroutine(dialogParser.ProcessDialogs());
    }
}
