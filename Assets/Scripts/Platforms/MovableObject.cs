using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Platforms
{
    public class MovableObject : MonoBehaviour
    {
        [SerializeField]
        GameObject maxLeftPosition;
        [SerializeField]
        GameObject maxRightPosition;
        [SerializeField]
        public AudioSource audioSource;

        GameObject player;
        NewInput input;
        bool playerInRange = false;
        bool objectInMove = false;

        private void Awake()
        {
            input = new NewInput();

            audioSource.Play();
            audioSource.Pause();
            input.Enable();
        }

        private void Update()
        {
            // Pochwycenie i puszczenie krzes�a
            if (input.Actions.Discard.triggered && objectInMove)
            {
                UnAttachFromPlayer();
            }
            else if (input.Actions.Discard.triggered)
            {
                AttachToPlayer();
            }
        }

        private void FixedUpdate()
        {
            // Sprawdzanie, czy gracz znajduje si� w zasi�gu i przypisywanie zmiennych
            if (playerInRange && player != null)
            {
                float horizontal = input.Movement.Horizontal.ReadValue<float>();

                // Poruszanie krzes�a
                if (objectInMove && horizontal != 0)
                {
                    if (horizontal > 0 && gameObject.transform.position.x < maxRightPosition.transform.position.x)
                    {
                        gameObject.transform.position += new Vector3(player.GetComponent<Character.CharacterController>().walkingSpeed * horizontal, 0, 0);
                    }
                    else if (horizontal < 0 && gameObject.transform.position.x > maxLeftPosition.transform.position.x)
                    {
                        gameObject.transform.position += new Vector3(player.GetComponent<Character.CharacterController>().walkingSpeed * horizontal, 0, 0);
                    }
                }

                // Sprawdzanie czy krzes�o si� porusza i uruchamianie d�wi�k�w szurania
                if (horizontal != 0 && objectInMove)
                {
                    audioSource.UnPause();
                }
                else
                {
                    audioSource.Pause();
                }
            }
        }

        //Metoda wywo�ywana przy podniesieniu przez gracza krzes�a, obs�uguje wszystkie jednorazowe efekty
        public void AttachToPlayer()
        {
            player.GetComponent<Character.CharacterController>().walkingSpeed = 0.025f;
            player.GetComponent<Character.CharacterController>().holdingObject = true;
            player.GetComponent<AudioSource>().Stop();

            objectInMove = true;
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
        }

        //Metoda wywo�ywana przy upuszczeniu przez gracza krzes�a, obs�uguje wszystkie jednorazowe efekty
        public void UnAttachFromPlayer()
        {
            objectInMove = false;

            gameObject.GetComponent<AudioSource>().Pause();
            player.GetComponent<Character.CharacterController>().walkingSpeed = 0.05f;
            player.GetComponent<Character.CharacterController>().holdingObject = false;
            player.GetComponent<AudioSource>().Play();
        }

        // Decydowanie czy gracz jest w zasi�gu
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
                UnAttachFromPlayer();
            }
        }
    }
}