using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SH.Enemy
{
    public class BossHand : MonoBehaviour
    {
        private static float hitTime = 0;

        private float cooldown = 0.5f;
        public Vector3 end = Vector3.zero;
        public BossVentsController controller;
        private Vector3 start;
        private float duration = -2f;
        private float time = 0;

        void Awake()
        {
            start = transform.position;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && hitTime + cooldown < Time.time)
            {
                hitTime = Time.time;
                controller.lives--;
                if (controller.lives == 0)
                {
                    Animator anim = GameObject.FindGameObjectWithTag("CrossfadeCanvas").GetComponentInChildren<Animator>();
                        StartCoroutine(Travel.SceneChanger.MovePlayerToScene(5, collision.gameObject, new Vector3(-1, -3, 0), Vector3.zero, anim));
                }
            }
        }

        void Update()
        {
            if (duration == -2f)
            {
                if (controller == null)
                {
                    Debug.LogError("No fight controller");
                    duration = -1f;
                }
                else
                    duration = controller.handMoveDuration;
            }
            if (time <= duration)
            {
                transform.position = Vector3.Lerp(start, end, time / duration);
                time += Time.deltaTime;
                if (time > duration)
                    time = duration;
            }
        }
    }
}
