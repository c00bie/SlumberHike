using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Managers
{
    // Klasa statyczna przechowuj�ca i zwracaj�ca, zmienne odpowiadaj�ce punktom kontrolnym w samej grze (skomplikowa�em j�, aby wprowadzi� bardziej uniwersaln� klas� - CheckPointChecker)
    public static class CheckPoints
    {
        public static Dictionary<string, bool> checkpoints = new Dictionary<string, bool>();

        // Metoda zwracaj�ca odpowiedni� zmienn� zale�nie od podanej nazwy
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
