using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Inventory
{
    public class ItemController : MonoBehaviour
    {
        private SpriteRenderer sr;
        private bool inRange = false;
        private NewInput input;

        [SerializeField]
        private bool mustBeGrounded = false;
        [SerializeField]
        private Interaction[] afterPickup;
        [SerializeField]
        private Item item = new Item();
        public Item Item
        {
            get => item;
            set 
            { 
                item = value;
                UpdateItem();
            }
        }

        private void UpdateItem()
        {
            if (item.image != null && sr != null)
            {
                if (inRange && item.inRangeImage != null)
                    sr.sprite = item.inRangeImage;
                else
                    sr.sprite = item.image;
            }
        }

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            input = new NewInput();
            input.Actions.Grab.Enable();
            if (item != null)
                UpdateItem();
        }

        private void Update()
        {
            if (inRange && input.Actions.Grab.IsPressed())
            {
                Inventory.AddItem(item);
                foreach (var item in afterPickup)
                    item.DoAction();
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (item != null && collision.gameObject.CompareTag("Player") && (!mustBeGrounded || collision.GetComponent<Character.CharacterController>().isGrounded))
            {
                //Debug.Log("Player in range!");
                inRange = true;
                if (item.inRangeImage != null && sr != null)
                    sr.sprite = item.inRangeImage;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (item != null && collision.gameObject.CompareTag("Player"))
            {
                //Debug.Log("Player out of range!");
                inRange = false;
                if (sr != null && sr.sprite != item.image)
                    sr.sprite = item.image;
            }
        }
    }
}