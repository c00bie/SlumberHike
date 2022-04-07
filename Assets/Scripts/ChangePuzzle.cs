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
    public GameObject cameraPosition;
    [SerializeField]
    private GameObject puzzl1;
    [SerializeField]
    private GameObject puzzl2;
    [SerializeField]
    private GameObject gameObject;
    [SerializeField]
    public Transform InstantiateMe;
    [SerializeField]
    GameObject player;
    int Player;


    void Awake()
    {

        Player = LayerMask.GetMask("Player");
        if (Puzzle.afterPuzzle)
        {
            mainCamera.transform.position = cameraPosition.transform.position;
            puzzl1.SetActive(false);
            puzzl2.SetActive(true);
            gameObject.SetActive(false);
            Puzzle.afterPuzzle = false;
        }
    }


    void FixedUpdate()
    {   
        if (this.GetComponents<Collider2D>()[0].IsTouchingLayers(Player) && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(this.indexLevel, LoadSceneMode.Single);
        }
    }




}
