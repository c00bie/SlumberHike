using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D col;

    [SerializeField]
    float jumpForce = 7f;
    [SerializeField]
    float movementSpeed = 4.5f;
    bool isGrounded = false;


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

    //Movement
    void Update()
    {
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
}
