using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    [InspectorName("Dialog Settings")]
    private DialogParser dialogParser = new DialogParser();
    [SerializeField]
    private List<Interaction> interactions = new List<Interaction>();

    private void Awake()
    {
        StartCoroutine(dialogParser.ProcessDialogs());
    }
}
