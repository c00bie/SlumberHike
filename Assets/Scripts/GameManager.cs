using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    bool equipmentVisible = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (equipmentVisible)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }

            equipmentVisible = !equipmentVisible;
        }
    }
}
