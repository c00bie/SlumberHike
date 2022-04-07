using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

//DO - Data operations
namespace DO
{
    //Class responsible for saving and loading game
    public static class SaveGame
    {
        public static void SavePlayer(GameObject player, Scene scene)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/save.wth";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(player, scene);

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
