using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CP - CheckPoints
namespace SH.Managers
{
    // Klasa odpowiadaj�ca za wywo�ywanie konkretnych funkcji zale�nie od podanych ustawie�
    // Sstara�em si� zrobi� jak najbardziej uniwersaln�, aby mo�na by�o u�ywa� jej na wszystkich obiektach.
    public class CheckPointChecker : MonoBehaviour
    {
        [SerializeField]
        string checkpointName;
        [SerializeField]
        string checkpointAction;

        [SerializeField]
        Sprite alternateImage;

        void Start()
        {
            if (CheckPoints.GetCheckPoint(checkpointName))
            {
                switch (checkpointAction)
                {
                    //Wywo�ywanie funkcjonalno�ci automatycznego wy��czania obiektu
                    case "DisableOnStart":
                        gameObject.SetActive(false);
                        break;

                    //Wywo�ywanie funkcjonalno�ci zmiany grafiki obiektu
                    case "ChangeSpriteOnStart":
                        gameObject.GetComponent<SpriteRenderer>().sprite = alternateImage;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
