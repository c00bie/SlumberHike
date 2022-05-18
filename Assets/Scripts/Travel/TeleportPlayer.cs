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

        bool playerInRange = false;
        NewInput input;
        GameObject player;
        Managers.SoundManager soundManager;

        private void Awake()
        {
            input = new NewInput();
            input.Enable();
        }

        //Sprawdzanie czy gracz jest w zasiêgu, przypisywanie do zmiennej jego oraz Ÿród³a muzyki
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
            //Teleportowanie gracza do po¿¹danej lokalizacji i wyzwalaj¹ca animacjê zakrywania ekranu
            if (playerInRange && input.Actions.Grab.triggered)
            {
                if (unlocked)
                {
                    StartCoroutine(TeleportPlayerCoroutine());
                }
            }
        }

        private IEnumerator TeleportPlayerCoroutine()
        {
            //Odtwarzanie efektu dŸwiêkowego (o ile taki istnieje)
            if (clip != null && soundManager != null)
            {
                soundManager.PlaySingleSound(clip);
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
        }
    }
}
