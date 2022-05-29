using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Enemy
{
    public class BirdSpawner : MonoBehaviour
    {
        [SerializeField]
        private Vector3[] localSpawnPoints = new Vector3[0];
        [SerializeField]
        private float distance = 10f;
        [SerializeField]
        private bool ltr = false;
        [SerializeField]
        private float delay = 1f;
        [SerializeField]
        private GameObject birdPrefab;
        [SerializeField]
        private float speed = 5f;

        bool canSpawn = false;
        float lastTime = 0f;
        AudioSource birdAudio;

        private void Start()
        {
            birdAudio = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (Managers.InGameMenuManager.gameIsPaused)
            {
                birdAudio.Pause();
            }
            else
            {
                birdAudio.UnPause();
            }

            if (canSpawn && lastTime + delay < Time.time && birdPrefab != null)
            {
                GameObject bird = Instantiate(birdPrefab, transform.TransformPoint(localSpawnPoints.RandomElement()), Quaternion.identity);
                BirdController ctrl = bird.GetComponent<BirdController>();
                ctrl.rightDirection = ltr;
                ctrl.distance = distance;
                ctrl.speed = speed;
                ctrl.enabled = true;
                lastTime = Time.time;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                canSpawn = true;
                birdAudio.Play();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                canSpawn = false;
                birdAudio.Stop();
            }
        }
    }
}