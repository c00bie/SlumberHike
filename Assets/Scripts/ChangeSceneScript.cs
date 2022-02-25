using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSceneScript : MonoBehaviour
{
    [SerializeField]
    public int indexLevel;
    [SerializeField]
    public float x;
    [SerializeField]
    public float y;



    int Player;


    void Awake()
    {

        Player = LayerMask.GetMask("Player");
    }


    void FixedUpdate()
    {
        if (this.GetComponents<Collider2D>()[0].IsTouchingLayers(Player))
        {
            Debug.Log("test");
            CharacterController.x = this.x;
            CharacterController.y = this.y;
            SceneManager.LoadScene(this.indexLevel, LoadSceneMode.Single);
        }
    }


}
