using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    [SerializeField]
    Texture2D idle;
    [SerializeField]
    Texture2D click;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("CursorChanger").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("SoundManager")[0].gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            Cursor.SetCursor(click, new Vector2(0, 0), CursorMode.Auto);
        else
            Cursor.SetCursor(idle, new Vector2(0, 0), CursorMode.Auto);
    }
}
