using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Platforms
{
    public class MovableObject : MonoBehaviour
    {
        GameObject player;
        NewInput input;
        bool playerInRange = false;

        private void Awake()
        {
            input = new NewInput();
            input.Enable();
        }

        private void Update()
        {
            // Pod³¹czanie obiektu do gracza pod warunkiem bycia w zasiêgu
            if (playerInRange && player != null)
            {
                if (input.Actions.Grab.triggered && this.gameObject.transform.GetChild(0).GetComponent<ChildDetector>().playerStanding == false && player.GetComponent<Character.CharacterController>().isGrounded)
                {
                    float leftX = gameObject.transform.GetChild(1).transform.position.x;
                    float rightX = gameObject.transform.GetChild(2).transform.position.x;

                    float leftDistance = Math.Abs((leftX - player.transform.position.x));
                    float rightDistance = Math.Abs((rightX - player.transform.position.x));

                    if (leftDistance < rightDistance)
                    {
                        player.transform.position = new Vector3(leftX, player.transform.position.y, player.transform.position.z);
                        player.GetComponent<Character.CharacterController>().cantMoveLeft = true;
                    }
                    else
                    {
                        player.transform.position = new Vector3(rightX, player.transform.position.y, player.transform.position.z);
                        player.GetComponent<Character.CharacterController>().cantMoveRight = true;
                    }

                    player.GetComponent<Character.CharacterController>().AttachObject(gameObject);
                }
            }
        }

        // Decydowanie czy gracz jest w zasiêgu
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                player = collision.gameObject;
                gameObject.GetComponentInChildren<ChildDetector>().player = player;
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
}