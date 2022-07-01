using SH.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCanvasController : MonoBehaviour
{
    [SerializeField]
    GameObject canvasObject;
    [SerializeField]
    MenuManager manager;
    [SerializeField]
    TMPro.TMP_InputField input;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.F11))
        {
            canvasObject.SetActive(!canvasObject.activeSelf);
        }
#endif
    }

    public void MoveToScene()
    {
        int val = 3;
        if (int.TryParse(input.text, out val))
            manager.NewGame(val);
        canvasObject.SetActive(false);
    }
}
