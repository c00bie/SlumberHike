using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SH.Travel
{
    public static class SceneChanger
    {
        //IEnumerator �aduj�cy now� scen�, ustawiaj�cy pozycj� kamery oraz zamykaj�cy star� scen�
        public static IEnumerator MoveToScene(int nextSceneId, Vector3 cameraPosition, Animator transition)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentId = currentScene.buildIndex;
            AsyncOperation nextScene = SceneManager.LoadSceneAsync(nextSceneId, LoadSceneMode.Additive);
            nextScene.allowSceneActivation = false;

            // W��czanie animacji zakrywania ekranu
            transition.SetTrigger("CoverTheScreen");

            yield return new WaitForSeconds(2f);
            if (transition.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                while (nextScene.progress < 0.9f)
                {
                    Debug.Log("Loading...");
                    yield return null;
                }

                nextScene.allowSceneActivation = true;

                Scene nextThisScene = SceneManager.GetSceneByBuildIndex(nextSceneId);

                while (!nextScene.isDone)
                {
                    Debug.Log("Almost there...");
                    yield return null;
                }

                SceneManager.SetActiveScene(nextThisScene);

                SceneManager.UnloadSceneAsync(currentId);

                Camera.main.gameObject.transform.position = cameraPosition;
            }
        }

        //IEnumerator �aduj�cy now� scen�, przenosz�cy na ni� gracza, ustawiaj�cy pozycj� kamery oraz zamykaj�cy star� scen�
        public static IEnumerator MovePlayerToScene(int nextSceneId, GameObject player, Vector3 position, Vector3 cameraPosition, Animator transition)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentId = currentScene.buildIndex;
            AsyncOperation nextScene = SceneManager.LoadSceneAsync(nextSceneId, LoadSceneMode.Additive);
            nextScene.allowSceneActivation = false;

            // W��czanie animacji zakrywania ekranu
            transition.SetTrigger("CoverTheScreen");

            yield return new WaitForSeconds(2f);
            if (transition.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                while (nextScene.progress < 0.9f)
                {
                    Debug.Log("Loading...");
                    yield return null;
                }

                nextScene.allowSceneActivation = true;

                Scene nextThisScene = SceneManager.GetSceneAt(1);

                SceneManager.MoveGameObjectToScene(player, nextThisScene);

                while (!nextScene.isDone)
                {
                    Debug.Log("Almost there...");
                    yield return null;
                }

                SceneManager.SetActiveScene(nextThisScene);

                SceneManager.UnloadScene(currentId);

                var ctrl = player.GetComponent<Character.CharacterController>();
                ctrl.transition = GameObject.FindGameObjectWithTag("CrossfadeCanvas").GetComponentInChildren<Animator>();
                player.transform.position = position;
                ctrl.ResumeMovement();
                GameObject currentCamera = GameObject.Find("Main Camera");
                currentCamera.transform.position = cameraPosition;

                TeleportPlayer.Teleporting = false;

                Data.SaveGame.SavePlayer(player, nextThisScene, cameraPosition);
            }
        }    
    }
}
