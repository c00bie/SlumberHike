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



    bool holdingObject = false;
    GameObject holdedObject;
    public bool isGrounded = false;
    Rigidbody2D rb;
    BoxCollider2D col;
    public static float x;
    public static float y = -3f;
    public Animator animator;
    private NewInput input;
    private SpriteRenderer spriteRenderer;
    bool crouched = false;


    void Start()
    {
        transform.position = new Vector3(x, y, 0f);
    }

    void Awake()
    {
        // Przypisywanie warto�ci zmiennym
        input = new NewInput();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        input.Enable();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Reset warunk�w skakania
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void Update()
    {
        // Od��czanie obiektu od gracza
        if (holdingObject)
        {
            if (input.Actions.Grab.IsPressed())
            {
                AttachObject(holdedObject);
            }
        }
    }

    void FixedUpdate()
    {
        float horizontal = input.Movement.Horizontal.ReadValue<float>();
        if (horizontal != 0)
            spriteRenderer.flipX = horizontal > 0;
        animator.SetFloat("Horizontal", horizontal);
        // Skakanie i wstawanie
        if (input.Movement.Jump.IsPressed() && holdingObject == false)
        {
            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                isGrounded = false;
            }
        }

        // Kucanie
        if (!crouched && input.Movement.Crouch.IsPressed() && holdingObject == false)
        {
            animator.SetBool("Crouch", true);
            col.offset = new Vector2(col.offset.x, -0.25f);
            col.size = new Vector2(col.size.x, 0.5f);
            crouched = true;
        }
        else if (crouched && !input.Movement.Crouch.IsPressed())
        {
            animator.SetBool("Crouch", false);
            col.offset = new Vector2(col.offset.x, 0);
            col.size = new Vector2(col.size.x, 4);
            crouched = false;
        }

        // Bieganie i chodzenie w prawo
        if (horizontal != 0)
        {
            if (input.Movement.Run.IsPressed() && holdingObject == false)
            {
                rb.velocity = new Vector2(runningSpeed * horizontal, rb.velocity.y);
            }
            else
            {
                rb.transform.position += new Vector3(walkingSpeed * horizontal, 0, 0);

            }
        }
    }

    // Metoda do��czaj�ca albo od��czaj�ca dany obiekt jako dziecko gracza
    public void AttachObject(GameObject movableObject)
    {
        if (holdingObject)
        {
            movableObject.transform.parent = null;

            holdingObject = false;
            holdedObject = null;

            walkingSpeed = 0.05f;
        }
        else
        {
            movableObject.transform.SetParent(gameObject.transform, true);

            holdingObject = true;
            holdedObject = movableObject;

            walkingSpeed = 0.025f;
        }
    }
}
 