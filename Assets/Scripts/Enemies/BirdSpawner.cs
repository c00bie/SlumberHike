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

        bool canSpawn = false;
        float lastTime = 0f;

        void Update()
        {
            if (canSpawn && lastTime + delay < Time.time && birdPrefab != null)
            {
                GameObject bird = Instantiate(birdPrefab, transform.TransformPoint(localSpawnPoints.RandomElement()), Quaternion.identity);
                BirdController ctrl = bird.GetComponent<BirdController>();
                ctrl.rightDirection = ltr;
                ctrl.isActive = true;
                ctrl.distance = distance;
                lastTime = Time.time;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                canSpawn = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                canSpawn = false;
        }
    }
}