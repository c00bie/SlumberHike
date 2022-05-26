using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Enemy
{
    public class BirdController : MonoBehaviour
    {
        public bool isActive = false;
        public float speed = 0.05f;
        public float distance = 10f;

        [SerializeField]
        private bool _rightDirection = true;
        public bool rightDirection
        {
            get { return _rightDirection; }
            set {
                _rightDirection = value;
                GetComponent<SpriteRenderer>().flipX = value;
            }
        }

        bool notAtacking = true;
        Rigidbody2D rb;
        Vector3 start;

        private void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            start = transform.position;
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
                var dist = Vector3.Distance(start, transform.position);
                if (dist >= distance)
                    Destroy(gameObject);
            }        
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            // "Killing" the player if he got touched 
            GameObject collidedObject = collider.gameObject;

            if (collidedObject.transform.CompareTag("Player") && notAtacking && !Travel.TeleportPlayer.Teleporting)
            {
                notAtacking = false;
                collidedObject.GetComponent<Character.CharacterController>().KillPlayer(gameObject.transform.name);
            }
        }
    }
}
