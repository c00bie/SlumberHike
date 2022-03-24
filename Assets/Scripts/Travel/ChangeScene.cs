using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//RC - Room Changing
namespace RC
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField]
        Vector3 position;
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
                StartCoroutine(MovePlayerToScene());
            }
        }

        //IEnumerator �aduj�cy now� scen�, przenosz�cy na ni� gracza oraz zamykaj�cy star� scen�
        IEnumerator MovePlayerToScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentId = currentScene.buildIndex;
            AsyncOperation nextScene = SceneManager.LoadSceneAsync(nextSceneId, LoadSceneMode.Additive);
            nextScene.allowSceneActivation = false;

            while (nextScene.progress < 0.9f)
            {
                Debug.Log("Loading...");
                yield return null;
            }

            nextScene.allowSceneActivation = true;

            Scene nextThisScene = SceneManager.GetSceneByBuildIndex(nextSceneId);

            player.transform.position = position;
            SceneManager.MoveGameObjectToScene(player, nextThisScene);

            while (!nextScene.isDone)
            {
                Debug.Log("Almost there...");
                yield return null;
            }

            SceneManager.SetActiveScene(nextThisScene);

            SceneManager.UnloadSceneAsync(currentId);
        }
    }
}
