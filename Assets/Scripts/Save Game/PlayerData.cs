using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//DO - Data operations
namespace DO
{
    //Class containing data that need to be saved
    [Serializable]
    public class PlayerData
    {
        public int levelId;
        public float[] position;
        public float[] cameraPosition;

        public PlayerData(GameObject player, Scene currentScene)
        {
            GameObject currentCamera = GameObject.Find("Main Camera");
            levelId = currentScene.buildIndex;

            position = new float[3];
            cameraPosition = new float[3];

            position[0] = player.transform.position.x;
            position[1] = player.transform.position.y;
            position[2] = player.transform.position.z;

            cameraPosition[0] = currentCamera.transform.position.x;
            cameraPosition[1] = currentCamera.transform.position.y;
            cameraPosition[2] = currentCamera.transform.position.z;
        }
    }
}
