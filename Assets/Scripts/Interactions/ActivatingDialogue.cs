using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SH.Dialogs;

namespace SH.Interactions
{
    public class ActivatingDialogue : Interaction
    {
        [SerializeField]
        private GameObject gameObject;
        public override bool IsAsync => true;
        public override void DoAction()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator DoActionAsync()
        {
            yield return gameObject.active = true;
        }
    }
}
