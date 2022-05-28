using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Interactions
{
    public class ChangeWalkingInteraction : Interaction
    {
        [SerializeField]
        AudioClip walkingClip;

        public override bool IsAsync => false;
        AudioSource characterAudioSource;

        private void Start()
        {
            characterAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }

        public override void DoAction()
        {
            characterAudioSource.clip = walkingClip;
            characterAudioSource.Play();
        }

        public override IEnumerator DoActionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
