using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Interactions
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ChangeSpriteInteraction : Interaction
    {
        [SerializeField]
        Sprite target;
        [SerializeField]
        Sprite reverse;
        [SerializeField]
        bool toggle = false;

        bool toggleState = false;
        SpriteRenderer spriteRenderer;

        public override bool IsAsync => false;

        public override void DoAction()
        {
            toggleState = !toggleState;
            if (toggle)
                spriteRenderer.sprite = toggleState ? target : reverse;
            else
                spriteRenderer.sprite = target;
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
