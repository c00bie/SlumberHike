using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 7f;
    [SerializeField]
    float runningSpeed = 4.5f;
    [SerializeField]
    float walkingSpeed = 0.05f;


    bool isGrounded = false;
    Rigidbody2D rb;
    BoxCollider2D col;
    public static float x;
    public static float y=-3f;
    public Animator animator;



    void Start()
    {
        SetPosition(x, y, 0.0f);

    }

    void Awake()
    {
        // Przypisywanie wartoœci zmiennym
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Reset warunków skakania
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void FixedUpdate()
    {
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        // Skakanie i wstawanie
        if (Input.GetKey(KeyCode.W))
        {
            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                isGrounded = false;
            }
        }

        // Kucanie
        if (Input.GetKey(KeyCode.S))
        {
            //animator.SetBool("Crouch", true);
            col.offset = new Vector2(col.offset.x, -0.25f);
            col.size = new Vector2(col.size.x, 0.5f);
        }
        else
        {
            //animator.SetBool("Crouch", false);
            col.offset = new Vector2(col.offset.x, 0);
            col.size = new Vector2(col.size.x, 1);
        }

        // Bieganie i chodzenie w prawo
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector2(runningSpeed, rb.velocity.y);
            }
            else
            {
                rb.transform.position += new Vector3(walkingSpeed, 0, 0);

            }
        }
        // Bieganie i chodzenie w lewo
        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector2(-runningSpeed, rb.velocity.y);
            }
            else
            {
                rb.transform.position -= new Vector3(walkingSpeed, 0, 0);
            }
        }
    }
    void SetPosition(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }
}
