using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildDetector : MonoBehaviour
{
    public GameObject player;
    public bool playerStanding = false;

    // Sprawdzanie, czy gracz stoi na platformie
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStanding = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStanding = false;
        }
    }

    private void Update()
    {
        // Warunek sprawdzaj�cy, czy gracz skacze albo stoi na platformie, aby pozwoli� graczowi zderzy� si� z platform�
        if (player.GetComponent<CharacterController>().isGrounded && playerStanding == false)
        {
            gameObject.layer = 6;
        }
        else if(player.transform.position.y > gameObject.transform.position.y + 1.95)
        {
            gameObject.layer = 0;
        }
    }
}
