using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Inventory
{
    public class ItemController : MonoBehaviour
    {
        private SpriteRenderer sr;
        private bool inRange = false;
        private bool inRangeUnpickable = false;
        private NewInput input;

        [SerializeField]
        private bool mustBeGrounded = false;
        [SerializeField]
        private Interactions.Interaction[] beforePickup;
        [SerializeField]
        private Interactions.Interaction[] afterPickup;
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
                StartCoroutine(PickUp());
            }
        }

        bool pickingUp = false;
        private IEnumerator PickUp()
        {
            if (pickingUp || Dialogs.DialogParser.IsRunning )
                yield break;
            pickingUp = true;
            foreach (var item in beforePickup)
                if (item.IsAsync)
                    yield return item.DoActionAsync();
                else
                    item.DoAction();
            Inventory.AddItem(item);
            foreach (var item in afterPickup)
                if (item.IsAsync)
                    yield return item.DoActionAsync();
                else
                    item.DoAction();
            Destroy(gameObject);
        }

        private IEnumerator CheckCollision(Collider2D collision)
        {
            var cc = collision.GetComponent<Character.CharacterController>();
            float start = Time.time;
            float check = start;
            if (mustBeGrounded)
            {
                yield return new WaitUntil(() =>
                {
                    check = Time.time;
                    return cc.isGrounded || !inRangeUnpickable || check - start > 5;
                });
            }
            if (inRangeUnpickable && check - start <= 5)
            {
                Debug.Log("Player in range!");
                inRange = true;
                if (item.inRangeImage != null && sr != null)
                    sr.sprite = item.inRangeImage;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (item != null && collision.gameObject.CompareTag("Player"))
            {
                inRangeUnpickable = true; 
                StartCoroutine(CheckCollision(collision));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (item != null && collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player out of range!");
                inRange = inRangeUnpickable = false;
                if (sr != null && sr.sprite != item.image)
                    sr.sprite = item.image;
            }
        }
    }
}