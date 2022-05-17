using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Managers
{
    // Klasa statyczna przechowuj¹ca i zwracaj¹ca, zmienne odpowiadaj¹ce punktom kontrolnym w samej grze (skomplikowa³em j¹, aby wprowadziæ bardziej uniwersaln¹ klasê - CheckPointChecker)
    public static class CheckPoints
    {
        public static bool puzzleCompleted = false;

        // Metoda zwracaj¹ca odpowiedni¹ zmienn¹ zale¿nie od podanej nazwy
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
