using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SH.Managers
{
    public class MenuManager : MonoBehaviour
    {
        System.Random random = new System.Random();
        Scene currentScene;
        bool glitchingMenuVisible = false;

        [SerializeField] GameObject glitchedMenu;
        [SerializeField] GameObject playerPrefab;
        [SerializeField] Button loadGameButton;
        [SerializeField] Animator transition;
        [SerializeField] ScriptableRendererFeature glitch;
        [SerializeField] ScriptableRendererData renderData;

        private void Awake()
        {
            currentScene = SceneManager.GetActiveScene();
            StartCoroutine(Glitch());
            // Sprawdzanie czy nale¿y aktywowaæ przycisk "Wczytaj grê" w menu g³ównym
            if (currentScene.name == "MainMenu" && File.Exists(Application.persistentDataPath + "/save.wth") == false)
            {
                loadGameButton.interactable = false;
            }
        }

        IEnumerator Glitch()
        {
            while (true)
            {
                glitchedMenu.SetActive(false);
                glitchingMenuVisible = false;
                glitch.SetActive(false);
                renderData.SetDirty();
                yield return new WaitForSecondsRealtime(Random.Range(.5f, 2.5f));
                glitchedMenu.SetActive(true);
                glitchingMenuVisible = true;
                glitch.SetActive(true);
                renderData.SetDirty();
                yield return new WaitForSecondsRealtime(Random.Range(.1f, 1f));
            }
        }

        private void Update()
        {
            // Obs³uga systemu zmiany grafik w menu g³ównym
            /*if (currentScene.name == "MainMenu")
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
            }*/
        }

        // Metody s³u¿¹ce do obs³ugi przycisków w menu g³ównym

        public void NewGame()
        {
            // Tworzenie gracza na pierwsz¹ scenê, kasowanie dotychczasowego zapisu oraz zakrywanie sceny
            transition.SetTrigger("CoverTheScreen");

            GameObject player = Instantiate(playerPrefab, new Vector3(0, -2.49f, 0), Quaternion.identity);
            glitch.SetActive(false);
            StopCoroutine(Glitch());
            renderData.SetDirty();
            StartCoroutine(Travel.SceneChanger.MovePlayerToScene(3, player, new Vector3(0, -2.49f, 0), new Vector3(0, 0, -10), transition));
            File.Delete(Application.persistentDataPath + "/save.wth");
        }
        public void LoadGame()
        {
            // Wczytywanie danych z zapisu, tworzenie gracza, za³adowywanie sceny, której numer zosta³ zapisany oraz zakrywanie sceny
            transition.SetTrigger("CoverTheScreen");

            Data.PlayerData data = Data.SaveGame.LoadPlayer();

            GameObject player = Instantiate(playerPrefab, new Vector3(data.position[0], data.position[1], data.position[2]), Quaternion.identity);
            StopCoroutine(Glitch());
            glitch.SetActive(false);
            renderData.SetDirty();
            StartCoroutine(Travel.SceneChanger.MovePlayerToScene(data.levelId, player, new Vector3(data.position[0], data.position[1], data.position[2]), new Vector3(data.cameraPosition[0], data.cameraPosition[1], data.cameraPosition[2]), transition));
        }
        public void Options()
        {
            // Wczytywanie menu opcji na wierzch menu g³ównego
            transition.SetTrigger("RevealTheScreen");
            SceneManager.LoadSceneAsync("MainOptions", LoadSceneMode.Additive);
        }
        public void QuitGame()
        {
            Debug.Log("Now game would close itself");
            Application.Quit();
        }

        // Metody s³u¿¹ce do obs³ugi przycisków w menu ustawieñ
        public void CloseOptions()
        {
            SceneManager.UnloadSceneAsync(currentScene.buildIndex);
        }
    }
}
