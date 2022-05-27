using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Managers
{
    public class GardenDoorManager : MonoBehaviour
    {
        void Start()
        {
            if (CheckPoints.GetCheckPoint("nightCompleted") && !CheckPoints.GetCheckPoint("barnCompleted"))
                gameObject.SetActive(false);
        }
    }
}