using SH.Travel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Interactions
{
    public class LeaveSceneInteraction : Interaction
    {
        [SerializeField]
        Vector3 position;
        [SerializeField]
        Vector3 cameraPosition;
        [SerializeField]
        int nextSceneId;
        [SerializeField]
        bool unlocked = true;
        [SerializeField]
        Animator transition;
        [SerializeField]
        AudioClip clip;

        Managers.SoundManager soundManager;
        GameObject player;

        public override bool IsAsync => true;

        private void Start()
        {
            soundManager = GameObject.Find("SoundManager")?.GetComponent<Managers.SoundManager>();
            player = GameObject.FindGameObjectWithTag("Player");
            if (transition == null)
                transition = GameObject.FindGameObjectWithTag("CrossfadeCanvas").GetComponentInChildren<Animator>();
        }

        public override void DoAction()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator DoActionAsync()
        {
            if (clip != null && soundManager != null)
            {
                soundManager.PlaySingleSound(clip);
            }

            return SceneChanger.MovePlayerToScene(nextSceneId, player, position, cameraPosition, transition);
        }
    }
}