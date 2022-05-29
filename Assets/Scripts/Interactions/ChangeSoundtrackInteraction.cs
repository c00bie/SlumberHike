using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Interactions
{
    public class ChangeSoundtrackInteraction : Interaction
    {
        [SerializeField]
        AudioClip soundtrackClip;

        public override bool IsAsync => false;
        Managers.SoundManager soundManager;

        private void Start()
        {
            soundManager = GameObject.Find("SoundManager").GetComponent<Managers.SoundManager>();
        }

        public override void DoAction()
        {
            soundManager.ChangeBackgroundMusic(soundtrackClip);
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
