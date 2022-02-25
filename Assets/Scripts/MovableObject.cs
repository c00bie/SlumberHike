using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public GameObject player;
    bool playerInRange = false;

    private void Update()
    {
        // Pod³¹czanie obiektu do gracza pod warunkiem bycia w zasiêgu
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && this.gameObject.transform.GetChild(0).GetComponent<ChildDetector>().playerStanding == false && player.GetComponent<CharacterController>().isGrounded)
            {
                float originalY = gameObject.transform.position.y;

                float leftX = gameObject.transform.GetChild(1).transform.position.x;
                float rightX = gameObject.transform.GetChild(2).transform.position.x;

                float leftDistance = Math.Abs((leftX - player.transform.position.x));
                float rightDistance = Math.Abs((rightX - player.transform.position.x));

                if (leftDistance < rightDistance)
                {
                    player.transform.position = new Vector3(leftX, player.transform.position.y, player.transform.position.z);
                }
                else
                {
                    player.transform.position = new Vector3(rightX, player.transform.position.y, player.transform.position.z);
                }

                player.GetComponent<CharacterController>().AttachObject(gameObject);
            }
        }
    }

    // Decydowanie czy gracz jest w zasiêgu
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}