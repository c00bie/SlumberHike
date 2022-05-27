using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Managers
{
    // Klasa statyczna przechowuj¹ca i zwracaj¹ca, zmienne odpowiadaj¹ce punktom kontrolnym w samej grze (skomplikowa³em j¹, aby wprowadziæ bardziej uniwersaln¹ klasê - CheckPointChecker)
    public static class CheckPoints
    {
        public static Dictionary<string, bool> checkpoints = new Dictionary<string, bool>();

        // Metoda zwracaj¹ca odpowiedni¹ zmienn¹ zale¿nie od podanej nazwy
        public static bool GetCheckPoint(string checkpointName)
        {
            return checkpoints.ContainsKey(checkpointName) && checkpoints[checkpointName];
        }

        public static void SetCheckPoint(string checkpointName)
        {
            if (!checkpoints.ContainsKey(checkpointName))
                checkpoints.Add(checkpointName, true);
            else
                checkpoints[checkpointName] = false;
        }
    }
}
