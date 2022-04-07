using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EC - Enemy Controller
namespace EC
{
    public class BirdController : MonoBehaviour
    {
        public bool isActive = false;
        public float speed = 0.05f;

        [SerializeField]
        bool rightDirection = true;

        bool notAtacking = true;
        Rigidbody2D rb;

        private void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            // Movement to left or right depending on boolean variable
            if (isActive)
            {
                if (rightDirection)
                {
                    rb.transform.position += new Vector3(speed, 0, 0);
                }
                else
                {
                    rb.transform.position -= new Vector3(speed, 0, 0);
                }
            }        
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            // "Killing" the player if he got touched in the ballz
            GameObject collidedObject = collider.gameObject;

            if (collidedObject.transform.CompareTag("Player") && notAtacking)
            {
                notAtacking = false;
                collidedObject.GetComponent<CM.CharacterController>().KillPlayer(gameObject.transform.name);
            }
        }
    }
}
