using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SH.Dialogs;

namespace SH.Interactions
{
    public class EnableComponentInteraction : Interaction
    {
        [SerializeField]
        private bool targetState = false;
        [SerializeField]
        private Behaviour target;
        public override bool IsAsync => false;
        public override void DoAction()
        {
            if (target != null)
                target.enabled = targetState;
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}