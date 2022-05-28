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
        [SerializeField]
        AudioClip optionalWalkingClip;
        [SerializeField]
        bool leaveOnEnter = false;

        GameObject player;
        NewInput input;
        bool playerInRange = false;
        Managers.SoundManager soundManager;
        bool started = false;

        private void Awake()
        {
            input = new NewInput();
            input.Enable();
        }

        private void Start()
        {
            soundManager = GameObject.Find("SoundManager")?.GetComponent<Managers.SoundManager>();
        }

        //Sprawdzanie czy gracz jest w zasi�gu, przypisywanie do zmiennej jego oraz �r�d�a muzyki
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.transform.CompareTag("Player"))
            {
                player = collision.gameObject;
                playerInRange = true;
            }

        }
        //Sprawdzanie czy gracz jest poza zasi�giem
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.transform.CompareTag("Player"))
            {
                playerInRange = false;
            }
        }

        private void Update()
        {
            //Wykrywanie czy gracz pr�buje przej�� na inn� scen� oraz puszczenie efektu d�wi�kowego (o ile takowy jest podany)
            if (playerInRange && (input.Actions.Grab.triggered || leaveOnEnter) && unlocked && !started && !Dialogs.DialogParser.IsRunning)
            {
                started = true;
                if (clip != null && soundManager != null)
                {
                    soundManager.PlaySingleSound(clip, 1);
                }

                // Opcjonalna zmiana d�wi�ku chodzenia
                if (optionalWalkingClip != null)
                {
                    player.GetComponent<AudioSource>().clip = optionalWalkingClip;
                    player.GetComponent<AudioSource>().Play();
                }

                StartCoroutine(SceneChanger.MovePlayerToScene(nextSceneId, player, position, cameraPosition, transition));
            }
        }
    }
}
