using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SH.Travel
{
    public class Puzzle : MonoBehaviour
    {
        [SerializeField]
        public int indexLevel;
        [SerializeField]
        GameObject playerPrefab;
        [SerializeField]
        Vector3 playerPosition;
        [SerializeField]
        Vector3 cameraPosition;
        [SerializeField]
        public Animator transition;

        [SerializeField]
        private Transform emptySpace = null;

        private Camera _camera;
        [SerializeField]
        private GameObject puzzle1;
        [SerializeField]
        private GameObject puzzle2;
        [SerializeField]
        private GameObject puzzle3;
        [SerializeField]
        private GameObject puzzle4;
        [SerializeField]
        private GameObject puzzle5;
        [SerializeField]
        private GameObject puzzle6;
        [SerializeField]
        private GameObject puzzle7;
        [SerializeField]
        private GameObject puzzle8;

        public Vector3 puzzle1Position;
        public Vector3 puzzle2Position;
        public Vector3 puzzle3Position;
        public Vector3 puzzle4Position;
        public Vector3 puzzle5Position;
        public Vector3 puzzle6Position;
        public Vector3 puzzle7Position;
        public Vector3 puzzle8Position;

        NewInput input;
        bool playerNotGenerated = true;

        void Awake()
        {
            // Ustawianie zmiennych odpowiadaj¹cych pozycjom puzzli
            puzzle1Position = puzzle1.transform.position;
            puzzle2Position = puzzle2.transform.position;
            puzzle3Position = puzzle3.transform.position;
            puzzle4Position = puzzle4.transform.position;
            puzzle5Position = puzzle5.transform.position;
            puzzle6Position = puzzle6.transform.position;
            puzzle7Position = puzzle7.transform.position;
            puzzle8Position = puzzle8.transform.position;

            input = new NewInput();
            input.Enable();
        }
        void Start()
        {
            _camera = gameObject.GetComponent<Camera>();
        }

        void Update()
        {
            //Zmiana pozycji puzzli po naciœniêciu myszk¹
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit)
                {
                    if (Vector2.Distance(emptySpace.position, hit.transform.position) < 2.5f)
                    {
                        Vector2 lastEmptySpacePosition = emptySpace.position;
                        Debug.Log(lastEmptySpacePosition);
                        emptySpace.position = hit.transform.position;
                        hit.transform.position = lastEmptySpacePosition;
                    }
                }
            }
            //U³o¿enie uk³adanki oraz zapis gry
            //(Jeœli nie chcecie rozwi¹zywaæ puzzli to mo¿ecie je skipn¹æ za pomoc¹ klawisza "e")
            if ((puzzle5.transform.position == puzzle1Position && puzzle7.transform.position == puzzle2Position && puzzle4.transform.position == puzzle3Position && puzzle3.transform.position == puzzle4Position && puzzle1.transform.position == puzzle5Position && puzzle8.transform.position == puzzle6Position && puzzle2.transform.position == puzzle7Position && puzzle6.transform.position == puzzle8Position) || input.Actions.Grab.triggered)
            {
                GameObject player = null;
                Managers.CheckPoints.puzzleCompleted = true;

                if (playerNotGenerated)
                {
                    player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
                }

                StartCoroutine(SceneChanger.MovePlayerToScene(indexLevel, player, playerPosition, cameraPosition, transition));

                Data.SaveGame.SavePlayer(player, SceneManager.GetSceneByBuildIndex(indexLevel), cameraPosition);
            }
        }
    }
}