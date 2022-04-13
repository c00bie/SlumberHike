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

    }


    void FixedUpdate()
    {   
        if (this.GetComponents<Collider2D>()[0].IsTouchingLayers(Player) && Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(this.indexLevel, LoadSceneMode.Additive);
            puzzl1.SetActive(false);
            puzzl2.SetActive(true);

        }
    }




}
