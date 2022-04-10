using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa odpowiadaj�ca za dzia�anie przedmiot�w
/// </summary>
public class Item : MonoBehaviour
{
    Inventory inventory;
    public GameObject itemButton;

    private void Start()
    {
        // Przypisanie warto�ci zmiennym
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Warunki, kt�re musz� zosta� spe�nione, aby podnie�� przedmiot
        if (collision.CompareTag("Player"))
        {
            if (MythicItems.backpack || inventory.isFull[0] == false)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    for (int i = 0; i < inventory.slots.Length; i++)
                    {
                        if (inventory.isFull[i] == false)
                        {
                            inventory.isFull[i] = true;
                            Instantiate(itemButton, inventory.slots[i].transform, false);
                            Destroy(gameObject);
                            break;
                        }
                    }
                }
            }
        }
    }
}
