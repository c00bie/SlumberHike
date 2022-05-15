using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//RC - Room Changing
namespace SH.Travel
{
    public class ChangePuzzle : MonoBehaviour
    {
        [SerializeField]
        public int indexLevel;

        [SerializeField]
        public Camera mainCamera;
        [SerializeField]
        public Vector3 cameraPosition;
        [SerializeField]
        Animator transition;
        bool playerInRange = false;
        NewInput input;
        GameObject player;

        void Awake()
        {
            input = new NewInput();
            input.Enable();
        }

        //Sprawdzanie czy gracz jest w zasi�gu
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

        void Update()
        {
            //Zmiana sceny pod warunkiem spe�nienia wymaga�
            if (playerInRange && input.Actions.Grab.triggered)
            {
                Data.SaveGame.SavePlayer(player, SceneManager.GetActiveScene(), Camera.main.transform.position);

                StartCoroutine(SceneChanger.MoveToScene(indexLevel, new Vector3(0.0399999991f, 25.6100006f, -10), transition));
            }
        }
    }
}
