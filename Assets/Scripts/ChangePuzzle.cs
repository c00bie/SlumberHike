using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//RC - Room Changing
namespace RC
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

        //Sprawdzanie czy gracz jest w zasiêgu
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

        void Update()
        {
            //Zmiana sceny pod warunkiem spe³nienia wymagañ
            if (playerInRange && input.Actions.Grab.triggered)
            {
                DO.SaveGame.SavePlayer(player, SceneManager.GetActiveScene(), Camera.main.transform.position);

                StartCoroutine(SH.Travel.SceneChanger.MoveToScene(indexLevel, new Vector3(0.0399999991f, 25.6100006f, -10), transition));
            }
        }
    }
}
