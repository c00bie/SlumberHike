using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CP - CheckPoints
namespace SH.Managers
{
    // Klasa odpowiadaj¹ca za wywo³ywanie konkretnych funkcji zale¿nie od podanych ustawieñ
    // Sstara³em siê zrobiæ jak najbardziej uniwersaln¹, aby mo¿na by³o u¿ywaæ jej na wszystkich obiektach.
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
                    //Wywo³ywanie funkcjonalnoœci automatycznego wy³¹czania obiektu
                    case CheckPointCheckerAction.DisableOnStart:
                        gameObject.SetActive(false);
                        break;

                    //Wywo³ywanie funkcjonalnoœci zmiany grafiki obiektu
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
