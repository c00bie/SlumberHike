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
        GameObject player;

        public override bool IsAsync => true;

        private void Start()
        {
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
            return SceneChanger.MovePlayerToScene(nextSceneId, player, position, cameraPosition, transition);
        }
    }
}