using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public GameObject item;
    Transform player;

    private void Start()
    {
        // Przypisywanie zmiennym wartoœci
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Metoda odpowiadaj¹ca za wygenerowanie wyrzuconego przedmiotu
    public void SpawnDroppedItem()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y);
        Instantiate(item, playerPos, Quaternion.identity);
    }
}
