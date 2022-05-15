using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//SH.Travel - Room Changing
namespace SH.Travel
{
    public class LeaveScene : MonoBehaviour
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

        GameObject player;
        NewInput input;
        bool playerInRange = false;
        AudioSource audioSource;

        private void Awake()
        {
            input = new NewInput();
            input.Enable();
        }

        private void Start()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = clip;
        }

        //Sprawdzanie czy gracz jest w zasiêgu oraz przypisywanie go do zmiennej
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.transform.CompareTag("Player"))
            {
                player = collision.gameObject;
                playerInRange = true;
            }

        }
        //Sprawdzanie czy gracz jest poza zasiêgiem
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.transform.CompareTag("Player"))
            {
                playerInRange = false;
            }
        }

        private void Update()
        {
            //Wykrywanie czy gracz próbuje przejœæ na inn¹ scenê
            if (playerInRange && input.Actions.Grab.triggered && unlocked)
            {
                StartCoroutine(MovePlayer());
            }
        }

        IEnumerator MovePlayer()
        {
            if (audioSource.clip != null)
            {
                audioSource.Play();
                yield return new WaitForSeconds(audioSource.clip.length - 1);
            }

            StartCoroutine(SceneChanger.MovePlayerToScene(nextSceneId, player, position, cameraPosition, transition));
        }
    }
}
