using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Interactions
{
    public class PlaySingleInteraction : Interaction
    {
        [SerializeField]
        AudioClip clip;
        [SerializeField]
        float volumeScaling;

        public override bool IsAsync => false;
        Managers.SoundManager soundManager;

        private void Start()
        {
            soundManager = GameObject.Find("SoundManager").GetComponent<Managers.SoundManager>();
        }

        public override void DoAction()
        {
            soundManager.PlaySingleSound(clip, volumeScaling);
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
