using SH.Travel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Interactions
{
    public class SetNightInteraction : Interaction
    {
        [SerializeField]
        bool isNight = true;
        public override bool IsAsync => false;
        public override void DoAction()
        {
            ChangeToNight.night = isNight;
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}