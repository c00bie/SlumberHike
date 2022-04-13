using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//RC - Room Changing
namespace RC
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
      
        GameObject player;
        NewInput input;
        bool playerInRange = false;

        private void Awake()
        {
            input = new NewInput();
            input.Enable();
        }

        //Sprawdzanie czy gracz jest w zasi�gu oraz przypisywanie go do zmiennej
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
            //Wykrywanie czy gracz pr�buje przej�� na inn� scen�
            if (playerInRange && input.Actions.Grab.triggered && unlocked)
            {
                StartCoroutine(SceneChanger.MovePlayerToScene(nextSceneId, player, position, cameraPosition));

                DO.SaveGame.SavePlayer(player, SceneManager.GetSceneByBuildIndex(nextSceneId));
            }
        }
    }
}
