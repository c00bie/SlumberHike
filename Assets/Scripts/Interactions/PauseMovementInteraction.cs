using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Interactions
{
    public class PauseMovementInteraction : Interaction
    {
        Character.CharacterController ctrl;
        public override bool IsAsync => false;
        [SerializeField]
        bool targetState = false;

        private void Start()
        {
            ctrl = GameObject.FindGameObjectWithTag("Player").GetComponent<Character.CharacterController>();
        }

        public override void DoAction()
        {
            if (ctrl == null)
                Start();
            if (targetState)
                ctrl?.PauseMovement();
            else
                ctrl?.ResumeMovement();
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}