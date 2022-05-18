using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SH.Dialogs;

namespace SH.Interactions
{
    public class ActiveInteraction : Interaction
    {
        [SerializeField]
        private bool targetState = false;
        public override bool IsAsync => false;
        public override void DoAction()
        {
            gameObject.SetActive(targetState);
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}