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
        bool glitchingMenuVisible = true;
        string musicFilePath;
        SoundManager soundManager;
        AudioClip audioClipForChange;
        Coroutine glitchCoroutine;
        [SerializeField] GameObject glitchedMenu;
        [SerializeField] GameObject playerPrefab;
        [SerializeField] GameObject menuPrefab;
        [SerializeField] GameObject inventoryPrefab;
        [SerializeField] Button loadGameButton;
        [SerializeField] Animator transition;
        [SerializeField] ScriptableRendererFeature glitch;
        [SerializeField] ScriptableRendererData renderData;
        [SerializeField] AudioClip woodWalkingSound;
        [SerializeField] AudioClip backgroundSound;

        private void Awake()
        {
            currentScene = SceneManager.GetActiveScene();
            glitchCoroutine = StartCoroutine(Glitch());
            // Sprawdzanie czy nale�y aktywowa� przycisk "Wczytaj gr�" w menu g��wnym
            if (currentScene.name == "MainMenu" && File.Exists(Application.persistentDataPath + "/save.wth") == false)
            {
                loadGameButton.interactable = false;
            }
            
            // Sprawdzenie czy należy zniszczyć obiekt menu w grze
            if (GameObject.FindGameObjectWithTag("InGameMenu") != null)
            {
                InGameMenuManager.toDelete = true;
            }

            GameObject soundManagerGameObject = GameObject.FindGameObjectWithTag("SoundManager");
            soundManager = soundManagerGameObject.GetComponent<SoundManager>();

            // Ustawianie ścieżki do plików muzycznych
            musicFilePath = "Sounds/";
        }

        IEnumerator Glitch()
        {
            while (glitchingMenuVisible)
            {
                glitchedMenu.SetActive(false);
                glitch.SetActive(false);
                renderData.SetDirty();
                yield return new WaitForSecondsRealtime(Random.Range(.5f, 2.5f));
                glitchedMenu.SetActive(true);
                glitch.SetActive(true);
                renderData.SetDirty();
                yield return new WaitForSecondsRealtime(Random.Range(.1f, 1f));
            }
            glitchedMenu.SetActive(false);
            glitchingMenuVisible = false;
            glitch.SetActive(false);
            renderData.SetDirty();
        }

        // Metody służące do obsługi przycisków w menu g��wnym
        public void NewGame(int scene = 3)
        {
            // Tworzenie gracza na pierwsz� scen�, kasowanie dotychczasowego zapisu oraz zakrywanie sceny
            transition.SetTrigger("CoverTheScreen");
            soundManager.ChangeBackgroundMusic(backgroundSound);
            Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity);
            GameObject player = Instantiate(playerPrefab, new Vector3(0, -2.49f, 0), Quaternion.identity);
            if (GameObject.FindGameObjectWithTag("InGameMenu") == null)
                Instantiate(menuPrefab, Vector3.zero, Quaternion.identity);
            glitchingMenuVisible = false;
            CursorChanger.CursorVisible = false;
            StartCoroutine(LoadWalkingClip("Chodzenie_ziemia", player));
            StartCoroutine(Travel.SceneChanger.MovePlayerToScene(scene, player, new Vector3(0, -2.49f, 0), new Vector3(0, 0, -10), transition));
            File.Delete(Application.persistentDataPath + "/save.wth");
        }
        public void LoadGame()
        {
            // Wczytywanie danych z zapisu, tworzenie gracza, za�adowywanie sceny, kt�rej numer zosta� zapisany oraz zakrywanie sceny
            transition.SetTrigger("CoverTheScreen");

            soundManager.ChangeBackgroundMusic(backgroundSound);

            Data.PlayerData data = Data.SaveGame.LoadPlayer();

            GameObject player = Instantiate(playerPrefab, new Vector3(data.position[0], data.position[1], data.position[2]), Quaternion.identity);
            glitchingMenuVisible = false;
            if (GameObject.FindGameObjectWithTag("InGameMenu") == null)
                Instantiate(menuPrefab, Vector3.zero, Quaternion.identity);

            // Ustawianie poprawnego odgłosu chodzenia
            StartCoroutine(LoadWalkingClip(data.walkingSoundClipName, player));

            // Ustawianie poprawnej muzyki do backgroundu
            StartCoroutine(LoadBackgroundMusic(data.backGroundClipName));

            CursorChanger.CursorVisible = false;
            StartCoroutine(Travel.SceneChanger.MovePlayerToScene(data.levelId, player, new Vector3(data.position[0], data.position[1], data.position[2]), new Vector3(data.cameraPosition[0], data.cameraPosition[1], data.cameraPosition[2]), transition));
        }
        public void Options()
        {
            transition.SetTrigger("RevealTheScreen");
            SceneManager.LoadSceneAsync("MainOptions", LoadSceneMode.Additive);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
        public void CloseOptions()
        {
            SceneManager.UnloadSceneAsync(currentScene.buildIndex);
        }

        //Korutyna, która odczytuje plik dźwiękowy z folderu i ustawia go w SoundManagerze
        IEnumerator LoadBackgroundMusic(string backgroundClipName)
        {
            StartCoroutine(GetAudioFromFile(backgroundClipName));

            while (audioClipForChange == null)
            {
                yield return new WaitForSeconds(0.1f);
            }

            soundManager.ChangeBackgroundMusic(audioClipForChange);
            audioClipForChange = null;
        }

        // Korutyna, która odczytuje plik dźwiękowy z folderu i dołącza go do AudioSource gracza
        IEnumerator LoadWalkingClip(string walkingClipName, GameObject player)
        {
            StartCoroutine(GetAudioFromFile(walkingClipName));

            while (audioClipForChange == null)
            {
                yield return new WaitForSeconds(0.1f);
            }

            player.GetComponent<AudioSource>().clip = audioClipForChange;
            player.GetComponent<AudioSource>().Play();
            audioClipForChange = null;
        }

        // Korutyna, która odzyskuje plik dźwiękowy z folderu "Resources" za pomocą nazwy pliku
        IEnumerator GetAudioFromFile(string fileName)
        {
            string pathToAudio = string.Format(musicFilePath + fileName);
            AudioClip newAudioClip = Resources.Load<AudioClip>(pathToAudio);

            audioClipForChange = newAudioClip;
            audioClipForChange.name = fileName;

            yield return null;
        }
    }
}
