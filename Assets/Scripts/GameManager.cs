using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Scene currentScene;
    public GameObject menu;
    public GameObject glitchedMenu;
    System.Random random = new System.Random();
    bool equipmentVisible = true;
    bool glitchingMenuVisible = false;

    // Kilka metod s³u¿¹cych do obs³ugi przycisków w menu
    public void NewGame()
    {
        SceneManager.LoadScene("P1");
    }

    public void QuitGame()
    {
        Debug.Log("Now game would close itself");
        Application.Quit();
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        // Obs³uga systemu zmiany grafik w menu g³ównym
        if (currentScene.name == "MainMenu")
        {
            if (glitchingMenuVisible == false)
            {
                int glitchChance = random.Next(10001);

                if (glitchChance > 9995)
                {
                    glitchedMenu.SetActive(true);
                    glitchingMenuVisible = true;
                }
            }
            else
            {
                int unglitchChance = random.Next(10001);

                if (unglitchChance > 9972)
                {
                    glitchedMenu.SetActive(false);
                    glitchingMenuVisible = false;
                }
            }    
        }

        // Obs³uga podnoszenia plecaka oraz uruchamiania ekwipunku
        if (MythicItems.backpack)
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
}
