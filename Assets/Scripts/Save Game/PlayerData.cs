using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//DO - Data operations
namespace SH.Data
{
    //Class containing data that need to be saved
    [Serializable]
    public class PlayerData
    {
        public int levelId;
        public float[] position;
        public float[] cameraPosition;
        public bool puzzleCompleted;

        public PlayerData(GameObject player, Scene currentScene, Vector3 currentCameraPosition)
        {
            levelId = currentScene.buildIndex;

            position = new float[3];
            cameraPosition = new float[3];

            position[0] = player.transform.position.x;
            position[1] = player.transform.position.y;
            position[2] = player.transform.position.z;

            cameraPosition[0] = currentCameraPosition.x;
            cameraPosition[1] = currentCameraPosition.y;
            cameraPosition[2] = currentCameraPosition.z;

            puzzleCompleted = Managers.CheckPoints.puzzleCompleted;
        }
    }
}
