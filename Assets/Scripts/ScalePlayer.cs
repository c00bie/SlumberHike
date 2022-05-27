using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Character
{
    public class ScalePlayer : MonoBehaviour
    {
        [SerializeField]
        private float scaleFactor = 0.45f;

        void OnEnable()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().ScalePlayer(scaleFactor);
        }
    }
}