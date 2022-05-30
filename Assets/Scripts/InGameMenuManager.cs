using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public GameObject pauseMenu;
        GameObject player;
        NewInput input;

        void Awake()
        {
            GameObject soudManagerGameObject = GameObject.FindGameObjectWithTag("SoundManager");
            soundManager = soudManagerGameObject.GetComponent<SoundManager>();
            input = new NewInput();
            input.Enable();
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
                if (!gameIsPaused)
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            pauseMenu.SetActive(false);
            CursorChanger.CursorVisible = false;
            Time.timeScale = 1;
            gameIsPaused = false;
        }

        void Pause()
        {
            pauseMenu.SetActive(true);
            CursorChanger.CursorVisible = true;
            Time.timeScale = 0;
            gameIsPaused = true;
        }

        public void ReturnToMenu()
        {
            CursorChanger.CursorVisible = true;
            gameIsPaused = false;
            Time.timeScale = 1;
            soundManager.ChangeBackgroundMusic(menuBackgroundMusic);

            StartCoroutine(Travel.SceneChanger.MoveToScene(0, new Vector3(0, 0, 0), transition));
        }

        public void QuitTheGame()
        {
            Application.Quit();
        }
    }
}
