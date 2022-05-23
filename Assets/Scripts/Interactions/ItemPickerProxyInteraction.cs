using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Interactions
{
    public class ItemPickerProxyInteraction : Interaction
    {
        [SerializeField]
        private Interaction[] validInteractions = new Interaction[0];
        [SerializeField]
        private Interaction[] invalidInteractions = new Interaction[0];
        [SerializeField]
        private string itemID;
        [SerializeField]
        private bool retryIfInvalid = true;
        [SerializeField]
        private Inventory.ItemPicker itemPicker;

        public override bool IsAsync => true;
        public override void DoAction()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator DoActionAsync()
        {
            if (itemPicker == null)
                itemPicker = GameObject.FindGameObjectWithTag("ItemPickerCanvas").GetComponent<Inventory.ItemPicker>();
        pick:
            yield return itemPicker.SelectItem();
            if (itemPicker.Exit)
                yield break;

            if (itemPicker.SelectedItem != null && itemPicker.SelectedItem.id == itemID || itemPicker.SelectedItem.id == "DEBUG_VALID")
            {
                Inventory.Inventory.UseItem(itemPicker.SelectedItem);
                foreach (Interaction validInteraction in validInteractions)
                {
                    if (validInteraction.IsAsync)
                        yield return validInteraction.DoActionAsync();
                    else
                        validInteraction.DoAction();
                }
                yield break;
            }

            foreach (Interaction invalidInteraction in invalidInteractions)
            {
                if (invalidInteraction.IsAsync)
                    yield return invalidInteraction.DoActionAsync();
                else
                    invalidInteraction.DoAction();
            }

            if (retryIfInvalid)
                goto pick;
        }
    }
}
