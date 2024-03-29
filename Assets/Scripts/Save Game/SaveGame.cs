using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SH.Data
{
    // Class responsible for saving and loading game
    public static class SaveGame
    {
        public static void SavePlayer(GameObject player, Scene scene, Vector3 cameraPosition)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/save.wth";
            FileStream stream = new FileStream(path, FileMode.Create);
            AudioSource audioSource = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>();

            PlayerData data = new PlayerData(player, scene, cameraPosition, audioSource);

            formatter.Serialize(stream, data);

            stream.Close();
        }

        public static PlayerData LoadPlayer()
        {
            string path = Application.persistentDataPath + "/save.wth";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;

                stream.Close();

                Managers.CheckPoints.checkpoints = data.checkpoints;

                return data;
            }
            else
            {
                Debug.LogError("Save file not found");
                return null;
            }
        }
    }
}
