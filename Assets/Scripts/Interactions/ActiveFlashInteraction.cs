using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SH.Dialogs;

namespace SH.Interactions
{
    public class ActiveFlashInteraction : Interaction
    {
        [SerializeField]
        private float waitForSeconds = 0.5f;
        [SerializeField]
        private bool initialState = false;
        public override bool IsAsync => true;
        public override void DoAction()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator DoActionAsync()
        {
            gameObject.SetActive(!initialState);
            yield return new WaitForSeconds(waitForSeconds);
            gameObject.SetActive(initialState);
        }
    }
}