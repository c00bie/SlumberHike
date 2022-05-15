using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SH.Travel
{
    public static class SceneChanger
    {
        //IEnumerator ³aduj¹cy now¹ scenê, ustawiaj¹cy pozycjê kamery oraz zamykaj¹cy star¹ scenê
        public static IEnumerator MoveToScene(int nextSceneId, Vector3 cameraPosition, Animator transition)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentId = currentScene.buildIndex;
            AsyncOperation nextScene = SceneManager.LoadSceneAsync(nextSceneId, LoadSceneMode.Additive);
            nextScene.allowSceneActivation = false;

            // W³¹czanie animacji zakrywania ekranu
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

        //IEnumerator ³aduj¹cy now¹ scenê, przenosz¹cy na ni¹ gracza, ustawiaj¹cy pozycjê kamery oraz zamykaj¹cy star¹ scenê
        public static IEnumerator MovePlayerToScene(int nextSceneId, GameObject player, Vector3 position, Vector3 cameraPosition, Animator transition)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentId = currentScene.buildIndex;
            AsyncOperation nextScene = SceneManager.LoadSceneAsync(nextSceneId, LoadSceneMode.Additive);
            nextScene.allowSceneActivation = false;

            // W³¹czanie animacji zakrywania ekranu
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

                SceneManager.MoveGameObjectToScene(player, nextThisScene);

                while (!nextScene.isDone)
                {
                    Debug.Log("Almost there...");
                    yield return null;
                }

                SceneManager.SetActiveScene(nextThisScene);

                SceneManager.UnloadScene(currentId);

                player.transform.position = position;
                GameObject currentCamera = GameObject.Find("Main Camera");
                currentCamera.transform.position = cameraPosition;
            }
        }    
    }
}
