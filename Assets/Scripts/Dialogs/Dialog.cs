using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField]
    private TextAsset dialogFile;

    public void ParseDialog()
    {
        Debug.Log(dialogFile.text);
    }
}
