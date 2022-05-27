using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Enemy
{
    public class BirdController : MonoBehaviour
    {
        public bool isActive = false;
        public float speed = 10f;
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
        Vector3 end;
        float time = 0;

        private void OnEnable()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            start = transform.position;
            end = transform.position + new Vector3(start.x + distance * (rightDirection ? 1 : -1), 0);
        }

        void Update()
        {
            if (time < speed)
            {
                rb.transform.position = Vector3.Lerp(start, end, time / speed);
                time += Time.deltaTime;
            }
            else
                Destroy(gameObject);
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
