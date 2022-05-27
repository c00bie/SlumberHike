using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SH.Dialogs;

namespace SH.Interactions
{
    public class RestartComponentInteraction : Interaction
    {
        [SerializeField]
        private Behaviour target;
        public override bool IsAsync => false;
        public override void DoAction()
        {
            if (target != null)
            {
                target.enabled = false;
                target.enabled = true;
            }
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}