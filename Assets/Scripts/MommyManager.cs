using SH.Travel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Managers
{
    public class MommyManager : MonoBehaviour
    {
        [SerializeField]
        private Sprite left;
        [SerializeField]
        private Sprite right;
        [SerializeField]
        private Sprite def;
        [SerializeField]
        private int targetCount = 5;
        private SpriteRenderer srenderer;
        private bool dir = false;
        private int count = 0;

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

        SoundManager soundManager;
        GameObject player;

        private void Start()
        {
            soundManager = GameObject.Find("SoundManager")?.GetComponent<SoundManager>();
            player = GameObject.FindGameObjectWithTag("Player");
            if (transition == null)
                transition = GameObject.FindGameObjectWithTag("CrossfadeCanvas").GetComponentInChildren<Animator>();
        }

        void OnEnable()
        {
            if (srenderer == null)
                srenderer = GetComponent<SpriteRenderer>();
            dir = !dir;
            srenderer.sprite = dir ? left : right;
            if (count++ == targetCount)
            {
                if (clip != null && soundManager != null)
                {
                    soundManager.PlaySingleSound(clip, 1);
                }
                CheckPoints.SetCheckPoint("nightCompleted");
                StartCoroutine(SceneChanger.MovePlayerToScene(nextSceneId, player, position, cameraPosition, transition));
            }
        }

        void OnDisable()
        {
            srenderer.sprite = def;
        }
    }
}