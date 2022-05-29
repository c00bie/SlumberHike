using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorChanger : MonoBehaviour
{
    public static bool CursorVisible { get; set; } = true;

    [SerializeField]
    Texture2D idle;
    [SerializeField]
    Texture2D click;
    [SerializeField]
    Sprite idleSprite;
    [SerializeField]
    Sprite clickSprite;
    [SerializeField]
    Vector3 offset = new Vector3(16, -16);

    Image cursor;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("CursorChanger").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("SoundManager")[0].gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
        cursor = GetComponent<Image>();
        Cursor.visible = false;
    }

    void Update()
    {
        if (!CursorVisible)
        {
            if (cursor.enabled)
                cursor.enabled = false;
            return;
        }
        if (!cursor.enabled)
            cursor.enabled = true;
        transform.position = Input.mousePosition + offset;
        cursor.sprite = Input.GetMouseButton(0) ? clickSprite : idleSprite;
        //if (Input.GetMouseButton(0))
        //    Cursor.SetCursor(click, new Vector2(0, 0), CursorMode.Auto);
        //else
        //    Cursor.SetCursor(idle, new Vector2(0, 0), CursorMode.Auto);
    }
}
