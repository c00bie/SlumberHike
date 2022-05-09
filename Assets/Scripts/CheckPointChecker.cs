using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CP - CheckPoints
namespace SH.Managers
{
    // Klasa odpowiadaj¹ca za wywo³ywanie konkretnych funkcji zale¿nie od podanych ustawieñ
    // Sstara³em siê zrobiæ jak najbardziej uniwersaln¹, aby mo¿na by³o u¿ywaæ jej na wszystkich obiektach.
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
                    //Wywo³ywanie funkcjonalnoœci automatycznego wy³¹czania obiektu
                    case "DisableOnStart":
                        gameObject.SetActive(false);
                        break;

                    //Wywo³ywanie funkcjonalnoœci zmiany grafiki obiektu
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
