using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SH.Travel
{
    public class ChangePuzzle : MonoBehaviour
    {
        [SerializeField]
        public int indexLevel;

        [SerializeField]
        public Camera mainCamera;
        [SerializeField]
        public Vector3 cameraPosition;
        [SerializeField]
        Animator transition;
        [SerializeField]
        Interactions.Interaction[] afterPuzzle = new Interactions.Interaction[0];

        bool playerInRange = false;
        NewInput input;
        GameObject player;
        bool puzzleLoaded = false;

        void Awake()
        {
            input = new NewInput();
            input.Enable();
        }

        //Sprawdzanie czy gracz jest w zasiêgu
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.transform.CompareTag("Player"))
            {
                player = collision.gameObject;
                playerInRange = true;
            }
        }
        //Sprawdzanie czy gracz jest poza zasiêgiem
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.transform.CompareTag("Player"))
            {
                playerInRange = false;
            }
        }

        void Update()
        {
            //Zmiana sceny pod warunkiem spe³nienia wymagañ
            if (!puzzleLoaded && playerInRange && input.Actions.Grab.triggered)
            {
                Data.SaveGame.SavePlayer(player, SceneManager.GetActiveScene(), Camera.main.transform.position);
                StartCoroutine(StartPuzzle());
                //StartCoroutine(SceneChanger.MoveToScene(indexLevel, new Vector3(0.0399999991f, 25.6100006f, -10), transition));
            }
        }

        IEnumerator StartPuzzle()
        {
            var load = SceneManager.LoadSceneAsync(indexLevel, LoadSceneMode.Additive);
            yield return new WaitUntil(() => load.isDone);
            puzzleLoaded = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(indexLevel));
            yield return new WaitUntil(() => Managers.CheckPoints.GetCheckPoint("puzzleCompleted"));
            foreach (var inter in afterPuzzle)
            {
                if (inter.IsAsync)
                    yield return inter.DoActionAsync();
                else
                    inter.DoAction();
            }
        }
    }
}
