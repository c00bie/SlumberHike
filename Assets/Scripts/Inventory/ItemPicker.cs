using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SH.Inventory
{
    public class ItemPicker : MonoBehaviour
    {
        [HideInInspector]
        public Item SelectedItem { get; private set; } = null;
        public bool Exit { get; private set; } = false;

        [SerializeField]
        private Image prevItem;
        [SerializeField]
        private Image mainItem;
        [SerializeField]
        private Image nextItem;
        [SerializeField]
        private TMPro.TMP_Text title;
        
        private Character.CharacterController characterController;
        private NewInput input;
        private float debounce = .1f;
        private float last = 0;
        private int index = 0;
        private Canvas canvas;

        void Awake()
        {
            characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<Character.CharacterController>();
            input = new NewInput();
            canvas = GetComponent<Canvas>();
        }

        void Update()
        {
            if (SelectedItem == null && !Exit && last + debounce < Time.time)
            {
                float value = input.Movement.Horizontal.ReadValue<float>();
                int change = (int)Mathf.Sign(value) * Mathf.CeilToInt(Mathf.Abs(value));
                if (!((change == -1 && index == 0) || (change == 1 && index == Inventory.Count - 1)))
                {
                    index += change;
                    ShowItems();
                }
                if (input.Dialogs.Accept.IsPressed())
                {
                    SelectedItem = Inventory.GetItem(index);
                    return;
                }
                if (input.Dialogs.Exit.IsPressed())
                {
                    Exit = true;
                    return;
                }
                last = Time.time;
            }
        }

        void ShowItems()
        {
            if (Inventory.Count == 0)
            {
                title.text = "Ekwipunek jest pusty";
                prevItem.sprite = nextItem.sprite = mainItem.sprite = null;
                prevItem.enabled = nextItem.enabled = mainItem.enabled = false;
                return;
            }

            if (index == 0)
            {
                prevItem.enabled = false;
                prevItem.sprite = null;
            }
            else
            {
                prevItem.enabled = true;
                prevItem.sprite = Inventory.GetItem(index - 1).inventoryImage;
            }

            if (index == Inventory.Count - 1)
            {
                nextItem.enabled = false;
                nextItem.sprite = null;
            }
            else
            {
                nextItem.enabled = true;
                nextItem.sprite = Inventory.GetItem(index + 1).inventoryImage;
            }

            Item curr = Inventory.GetItem(index);
            mainItem.enabled = true;
            mainItem.sprite = curr.inventoryImage;
            title.text = curr.name;
        }

        public IEnumerator SelectItem()
        {
            index = 0;
            SelectedItem = null;
            Exit = false;
            ShowItems();
            characterController.PauseMovement();
            input.Movement.Horizontal.Enable();
            input.Dialogs.Accept.Enable();
            input.Dialogs.Exit.Enable();
            canvas.enabled = true;
            yield return new WaitWhile(() => SelectedItem == null && !Exit);
            input.Movement.Horizontal.Disable();
            input.Dialogs.Accept.Disable();
            input.Dialogs.Exit.Disable();
            characterController.ResumeMovement();
            canvas.enabled = false;
        }
    }
}
