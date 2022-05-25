using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToNight : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> toDeactivate;
    [SerializeField]
    public List<GameObject> toActivate;

    public static bool night = false;

    void Start()
    {
        foreach (GameObject myListChild in toDeactivate)
        {

            myListChild.SetActive(!night);
        }
        foreach (GameObject myListChild in toActivate)
        {

            myListChild.SetActive(night);
        }
    }


    void Update()
    {
        
    }
}
