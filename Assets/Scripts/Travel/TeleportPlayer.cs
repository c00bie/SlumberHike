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

        bool playerInRange = false;
        NewInput input;
        GameObject player;

        private void Awake()
        {
            input = new NewInput();
            input.Enable();
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
            //Teleportowanie gracza do po¿¹danej lokalizacji
            if (playerInRange && input.Actions.Grab.triggered)
            {
                if (unlocked)
                {
                    mainCamera.transform.position = cameraPosition.transform.position;
                    player.transform.position = teleportTo;
                }
            }
        }
    }
}
