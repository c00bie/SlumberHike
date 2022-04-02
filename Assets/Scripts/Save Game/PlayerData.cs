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

        public PlayerData(GameObject player, Scene currentScene)
        {
            levelId = currentScene.buildIndex;

            position = new float[3];

            position[0] = player.transform.position.x;
            position[1] = player.transform.position.y;
            position[2] = player.transform.position.z;
        }
    }
}
