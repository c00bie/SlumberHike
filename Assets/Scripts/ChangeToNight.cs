using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToNight : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> toDisactive;
    [SerializeField]
    public List<GameObject> toActive;

    public static bool night = false;

    void Start()
    {
        if (night == true)
        {
            foreach (GameObject myListChild in toDisactive)
            {

                myListChild.active = false;
            }
            foreach (GameObject myListChild in toActive)
            {

                myListChild.active = true;
            }
        }
        

    }


    void Update()
    {
        
    }
}
