using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Travel
{
    public class TeleportPlayer : MonoBehaviour
    {
        [SerializeField]
        Camera mainCamera;
        [SerializeField]
        Vector3 teleportTo;
        [SerializeField]
        GameObject cameraPosition;
        [SerializeField]
        bool unlocked = true;
        [SerializeField]
        Animator transition;
        [SerializeField]
        AudioClip clip;
        [SerializeField]
        AudioClip backgroundMusicSwapClip;
        [SerializeField]
        AudioClip optionalWalkingClip;

        bool playerInRange = false;
        NewInput input;
        GameObject player;
        Managers.SoundManager soundManager;

        public static bool Teleporting { get; private set; } = false;

        private void Awake()
        {
            input = new NewInput();
            input.Enable();
        }

        //Sprawdzanie czy gracz jest w zasi�gu, przypisywanie do zmiennej jego oraz �r�d�a muzyki
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                player = collision.gameObject;
                playerInRange = true;

                if (GameObject.Find("SoundManager") != null)
                {
                    soundManager = GameObject.Find("SoundManager").GetComponent<Managers.SoundManager>();
                }
            }

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            playerInRange = false;
        }

        private void Update()
        {
            //Teleportowanie gracza do po��danej lokalizacji i wyzwalaj�ca animacj� zakrywania ekranu
            if (playerInRange && input.Actions.Grab.triggered)
            {
                if (unlocked && !Dialogs.DialogParser.IsRunning)
                {
                    StartCoroutine(TeleportPlayerCoroutine());

                    // Opcjonalne zmiana muzyki w tle
                    if (backgroundMusicSwapClip != null)
                    {
                        soundManager.ChangeBackgroundMusic(backgroundMusicSwapClip);
                    }

                    // Opcjonalna zmiana d�wi�ku chodzenia
                    if (optionalWalkingClip != null)
                    {
                        player.GetComponent<AudioSource>().clip = optionalWalkingClip;
                        player.GetComponent<AudioSource>().Play();
                    }
                }
            }
        }

        private IEnumerator TeleportPlayerCoroutine()
        {
            Teleporting = true;
            //Odtwarzanie efektu d�wi�kowego (o ile taki istnieje)
            if (clip != null && soundManager != null)
            {
                soundManager.PlaySingleSound(clip, 1);
            }

            //Przyciemnianie ekranu oraz teleportowanie gracza
            transition.SetTrigger("CoverTheScreen");
            yield return new WaitForSeconds(2f);
            if (transition.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                mainCamera.transform.position = cameraPosition.transform.position;
                player.transform.position = teleportTo;
            }
            //Debug.Log(transition.GetCurrentAnimatorStateInfo(0).normalizedTime);
            transition.SetTrigger("RevealTheScreen");
            Teleporting = false;
        }
    }
}
