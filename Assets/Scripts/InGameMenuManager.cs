using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SH.Managers
{
    public class InGameMenuManager : MonoBehaviour
    {
        [SerializeField]
        Animator transition;
        [SerializeField]
        AudioClip menuBackgroundMusic;

        SoundManager soundManager;
        public static bool gameIsPaused = false;
        public static bool toDelete = false;
        public static bool alreadyExisting = false;
        public GameObject pauseMenu;
        GameObject player;
        NewInput input;

        void Awake()
        {
            if (alreadyExisting)
            {
                Destroy(gameObject);
            }
            else
            {
                alreadyExisting = true;
            }

            GameObject soudManagerGameObject = GameObject.FindGameObjectWithTag("SoundManager");
            soundManager = soudManagerGameObject.GetComponent<SoundManager>();
            input = new NewInput();
            input.Enable();
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            else
            {
                if (gameIsPaused)
                {
                    player.GetComponent<AudioSource>().Pause();
                }
            }

            if (input.Actions.OpenGameMenu.triggered)
            {
                if (!gameIsPaused && !Inventory.ItemPicker.IsOpen)
                {
                    Pause();
                }
            }
            if (transition == null)
                transition = GameObject.FindGameObjectWithTag("CrossfadeCanvas")?.GetComponentInChildren<Animator>();
        }

        public void Resume()
        {
            pauseMenu.SetActive(false);
            CursorChanger.CursorVisible = false;
            Time.timeScale = 1;
            gameIsPaused = false;
            if (!Dialogs.DialogParser.IsRunning)
                player?.GetComponent<Character.CharacterController>()?.ResumeMovement();
        }

        void Pause()
        {
            pauseMenu.SetActive(true);
            CursorChanger.CursorVisible = true;
            Time.timeScale = 0;
            gameIsPaused = true;
            player?.GetComponent<Character.CharacterController>()?.PauseMovement();
        }

        public void ReturnToMenu()
        {
            CursorChanger.CursorVisible = true;
            gameIsPaused = false;
            Time.timeScale = 1;
            soundManager.ChangeBackgroundMusic(menuBackgroundMusic);
            pauseMenu.SetActive(false);
            StartCoroutine(Travel.SceneChanger.MoveToScene(0, new Vector3(0, 0, 0), transition));
        }

        public void QuitTheGame()
        {
            Application.Quit();
        }
    }
}
