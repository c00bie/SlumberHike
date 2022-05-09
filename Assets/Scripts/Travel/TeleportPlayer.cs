using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//RC - Room Changing
namespace RC
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
            player = collision.gameObject;
            playerInRange = true;
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
            //Odtwarzanie efektów dŸwiêkowych
            audioSource.Play();

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
