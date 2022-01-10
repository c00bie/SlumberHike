using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 7f;
    [SerializeField]
    float movementSpeed = 4.5f;


    bool isGrounded = false;
    Rigidbody2D rb;
    BoxCollider2D col;
    public static float x;
    public static float y=-3f;


    void Start()
    {
        SetPosition(x, y, 0.0f);

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void FixedUpdate()
    {
        //Movement
        if (Input.GetKey(KeyCode.W))
        {
            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                isGrounded = false;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            col.offset = new Vector2(col.offset.x, -0.25f);
            col.size = new Vector2(col.size.x, 0.5f);
        }
        else
        {
            col.offset = new Vector2(col.offset.x, 0);
            col.size = new Vector2(col.size.x, 1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
        }
    }
    void SetPosition(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }
}
