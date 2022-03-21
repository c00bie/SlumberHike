using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePuzzle : MonoBehaviour
{

    [SerializeField]
    private GameObject puzzl1;
    [SerializeField]
    private GameObject puzzl2;


    public static bool x = true;
    public static bool y = false;



    void Start()
    {
        puzzl1.SetActive(x);
        puzzl2.SetActive(y);
    }

    void Update()
    {
        
    }
}
