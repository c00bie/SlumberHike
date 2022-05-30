using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMenu : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(SH.Travel.SceneChanger.MoveToScene(0, new Vector3(0, 0, 0), GameObject.FindGameObjectWithTag("CrossfadeCanvas").GetComponentInChildren<Animator>()));
    }
}
