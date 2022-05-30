using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Interactions
{
    public class ButterflyVisibilityInteraction : Interaction
    {
        GameObject motylek;
        public override bool IsAsync => false;
        [SerializeField]
        bool targetState = true;
        public override void DoAction()
        {
            if (motylek == null)
                Start();
            motylek.SetActive(targetState);
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }

        void Start()
        {
            motylek = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
        }

        
    }
}