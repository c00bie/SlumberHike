using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//SH.Travel - Room Changing
namespace SH.Travel
{
    public class ChangeCameraPosition : MonoBehaviour
    {
        [SerializeField]
        Camera kamera;
        [SerializeField]
        GameObject roomPosition;
        [SerializeField]
        GameObject nextRoomPosition;
        [SerializeField]
        bool onlyOneWay = true;
        [SerializeField]
        bool vertical = false;

        private void OnTriggerExit2D(Collider2D collision)
        {

            // Kod wykrywaj¹cy próbê przejœcia do nastêpnego pokoju oraz zmieniaj¹cy po³o¿enie kamery
            if (collision.gameObject.transform.CompareTag("Player"))
            {
                GameObject player = collision.gameObject;

                if ((gameObject.transform.position.x > player.transform.position.x && vertical == false) || (gameObject.transform.position.y < player.transform.position.y && vertical))
                {
                    kamera.transform.position = new Vector3(nextRoomPosition.transform.position.x, nextRoomPosition.transform.position.y, kamera.transform.position.z);
                    
                    if (onlyOneWay)
                    {
                        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                    }
                }
                else
                {
                    kamera.transform.position = new Vector3(roomPosition.transform.position.x, roomPosition.transform.position.y, kamera.transform.position.z);
                }
            }
        }
    }
}
