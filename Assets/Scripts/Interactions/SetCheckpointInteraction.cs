using SH.Travel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Interactions
{
    public class SetCheckpointInteraction : Interaction
    {
        [SerializeField]
        string checkpointName;
        public override bool IsAsync => false;
        public override void DoAction()
        {
            Managers.CheckPoints.SetCheckPoint(checkpointName);
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}