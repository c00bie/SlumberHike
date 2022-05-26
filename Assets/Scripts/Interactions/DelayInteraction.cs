using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Interactions
{
    public class DelayInteraction : Interaction
    {
        [SerializeField]
        private float delay = 5f;
        public override bool IsAsync => true;
        public override void DoAction()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator DoActionAsync()
        {
            return new WaitForSecondsRealtime(delay);
        }
    }
}