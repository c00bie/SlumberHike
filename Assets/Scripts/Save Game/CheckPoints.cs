using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Managers
{
    // Klasa statyczna przechowuj�ca i zwracaj�ca, zmienne odpowiadaj�ce punktom kontrolnym w samej grze (skomplikowa�em j�, aby wprowadzi� bardziej uniwersaln� klas� - CheckPointChecker)
    public static class CheckPoints
    {
        public static bool puzzleCompleted = false;

        // Metoda zwracaj�ca odpowiedni� zmienn� zale�nie od podanej nazwy
        public static bool GetCheckPoint(string checkpointName)
        {
            switch (checkpointName)
            {
                case "Puzzle":
                    return puzzleCompleted;

                default:
                    return false;
            }
        }
    }
}
