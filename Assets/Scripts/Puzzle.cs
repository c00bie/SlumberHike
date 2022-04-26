using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle : MonoBehaviour
{
    [SerializeField]
    public int indexLevel;
    [SerializeField]
    Vector3 position;
    [SerializeField]
    Vector3 cameraPosition;

    GameObject player;

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
    //[SerializeField]
    //private GameObject player;



    public Vector3 puzzle1Position;
    public Vector3 puzzle2Position;
    public Vector3 puzzle3Position;
    public Vector3 puzzle4Position;
    public Vector3 puzzle5Position;
    public Vector3 puzzle6Position;
    public Vector3 puzzle7Position;
    public Vector3 puzzle8Position;

    void Awake()
    {
        puzzle1Position = puzzle1.transform.position;
        puzzle2Position = puzzle2.transform.position;
        puzzle3Position = puzzle3.transform.position;
        puzzle4Position = puzzle4.transform.position;
        puzzle5Position = puzzle5.transform.position;
        puzzle6Position = puzzle6.transform.position;
        puzzle7Position = puzzle7.transform.position;
        puzzle8Position = puzzle8.transform.position;
    }
    void Start()
    {
        //_camera = Camera.main;
        _camera = gameObject.GetComponent<Camera>();
        player = GameObject.FindWithTag("Player");

        player.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                //Debug.Log(puzzle1.position.x);
                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 2.5f)
                {
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    Debug.Log(lastEmptySpacePosition);
                    emptySpace.position = hit.transform.position;
                    hit.transform.position = lastEmptySpacePosition;
                }
            }
        }
        if ((puzzle5.transform.position == puzzle1Position && puzzle7.transform.position == puzzle2Position && puzzle4.transform.position == puzzle3Position && puzzle3.transform.position == puzzle4Position && puzzle1.transform.position == puzzle5Position && puzzle8.transform.position == puzzle6Position && puzzle2.transform.position == puzzle7Position && puzzle6.transform.position == puzzle8Position)|| Input.GetKeyDown(KeyCode.X))
        {
            //SceneManager.UnloadSceneAsync(2);
            player.SetActive(true);

            RC.SceneChanger.MovePlayerToScene(indexLevel, player, position, cameraPosition);
        }

    }
}