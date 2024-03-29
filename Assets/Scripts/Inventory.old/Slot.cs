using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    Inventory inventory;
    public int i;

    private void Start()
    {
        // Przypisywanie zmiennym wartości
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        // Odblokowywanie slotów ekwipunku
        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }

    // Metoda odpowiadająca za upuszczanie przedmiotów
    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<SpawnItems>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
        }
    }
}
