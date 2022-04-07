using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//RC - Room Changing
namespace RC
{
    public static class SceneChanger
    {
        //IEnumerator �aduj�cy now� scen�, przenosz�cy na ni� gracza oraz zamykaj�cy star� scen�
        public static IEnumerator MovePlayerToScene(int nextSceneId, GameObject player, Vector3 position)
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

            
            
            SceneManager.MoveGameObjectToScene(player, nextThisScene);
            player.transform.position = position;

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
