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
            //Wykrywanie czy gracz próbuje przejœæ na inn¹ scenê
            if (playerInRange && input.Actions.Grab.triggered && unlocked)
            {
                StartCoroutine(MovePlayerToScene());
            }
        }

        //IEnumerator ³aduj¹cy now¹ scenê, przenosz¹cy na ni¹ gracza oraz zamykaj¹cy star¹ scenê
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
