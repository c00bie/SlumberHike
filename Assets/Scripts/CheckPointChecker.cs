using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CP - CheckPoints
namespace SH.Managers
{
    // Klasa odpowiadaj�ca za wywo�ywanie konkretnych funkcji zale�nie od podanych ustawie�
    // Sstara�em si� zrobi� jak najbardziej uniwersaln�, aby mo�na by�o u�ywa� jej na wszystkich obiektach.
    [DefaultExecutionOrder(-1)]
    public class CheckPointChecker : MonoBehaviour
    {
        [SerializeField]
        string checkpointName;
        [SerializeField]
        CheckPointCheckerAction checkpointAction;
        [SerializeField]
        bool inverse = false;

        [SerializeField]
        Sprite alternateImage;
        [SerializeField]
        Behaviour component;

        void OnEnable()
        {
            if (CheckPoints.GetCheckPoint(checkpointName) ^ inverse)
            {
                switch (checkpointAction)
                {
                    //Wywo�ywanie funkcjonalno�ci automatycznego wy��czania obiektu
                    case CheckPointCheckerAction.DisableOnStart:
                        gameObject.SetActive(false);
                        break;

                    //Wywo�ywanie funkcjonalno�ci zmiany grafiki obiektu
                    case CheckPointCheckerAction.ChangeSpriteOnStart:
                        gameObject.GetComponent<SpriteRenderer>().sprite = alternateImage;
                        break;

                    case CheckPointCheckerAction.DisableComponentOnStart:
                        if (component != null)
                            component.enabled = false;
                        break;

                    default:
                        break;
                }
            }
        }
    }

    public enum CheckPointCheckerAction
    {
        DisableOnStart,
        ChangeSpriteOnStart,
        DisableComponentOnStart
    }
}
