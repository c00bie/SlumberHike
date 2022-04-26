using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangePuzzle : MonoBehaviour
{
    [SerializeField]
    public int indexLevel;

    [SerializeField]
    public Camera mainCamera;
    [SerializeField]
    public Vector3 position;
    [SerializeField]
    public Vector3 cameraPosition;
    [SerializeField]
    private GameObject puzzl1;
    [SerializeField]
    private GameObject puzzl2;
    //[SerializeField]
    //private GameObject gameObject;
    //[SerializeField]
    //public Transform InstantiateMe;
    //[SerializeField]
    GameObject player;
    bool playerInRange = false;
    //int Player;

    NewInput input;

    void Awake()
    {

        //Player = LayerMask.GetMask("Player");
        input = new NewInput();
        input.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject;
        playerInRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
    }

    void Update()
    {   
        if (playerInRange && input.Actions.Grab.triggered)
        {
            //gameObject.SetActive(false);
            //SceneManager.LoadScene(this.indexLevel, LoadSceneMode.Additive);
            //puzzl1.SetActive(false);
            //puzzl2.SetActive(true);

            StartCoroutine(RC.SceneChanger.MovePlayerToScene(indexLevel, player, player.transform.position, new Vector3(0.0399999991f, 25.6100006f, -10)));

        }
    }




}
