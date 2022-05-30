using SH.Travel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Interactions
{
    public class MoveToSceneInteraction : Interaction
    {
        [SerializeField]
        Vector3 cameraPosition;
        [SerializeField]
        int nextSceneId;
        [SerializeField]
        Animator transition;

        public override bool IsAsync => false;

        private void Start()
        {
            if (transition == null)
                transition = GameObject.FindGameObjectWithTag("CrossfadeCanvas").GetComponentInChildren<Animator>();
        }

        public override void DoAction()
        {
            StartCoroutine(SceneChanger.MoveToScene(nextSceneId, cameraPosition, transition));
        }

        public override IEnumerator DoActionAsync()
        {
            return SceneChanger.MoveToScene(nextSceneId, cameraPosition, transition);
        }
    }
}