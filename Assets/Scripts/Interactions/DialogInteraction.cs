using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SH.Dialogs;

namespace SH.Interactions
{
    public class DialogInteraction : Interaction
    {
        [SerializeField]
        private DialogParser dialogParser = new DialogParser();
        public override bool IsAsync => true;
        public override void DoAction()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator DoActionAsync()
        {
            yield return dialogParser.ParseDialogs();
            yield return dialogParser.ProcessDialogs();
        }
    }
}