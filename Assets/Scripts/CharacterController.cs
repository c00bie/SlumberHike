using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

//CM - Character Movement
namespace CM
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField]
        float jumpForce = 7f;
        [SerializeField]
        float runningSpeed = 4.5f;
        [SerializeField]
        float walkingSpeed = 0.05f;



        bool holdingObject = false;
        public bool cantMoveRight = false;
        public bool cantMoveLeft = false;
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
                animator.SetBool("Jump", false);
            }
        }

        private void Update()
        {
            // Od��czanie obiektu od gracza
            if (holdingObject)
            {
                if (input.Actions.Discard.triggered)
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
                    animator.SetBool("Jump", true);
                    rb.velocity = Vector2.up * jumpForce;
                    isGrounded = false;
                }
            }

            // Kucanie
            if (!crouched && input.Movement.Crouch.IsPressed() && holdingObject == false)
            {
                animator.SetBool("Crouch", true);
                col.offset = new Vector2(col.offset.x, -col.size.y / 4f);
                col.size = new Vector2(col.size.x, col.size.y / 2);
                crouched = true;
            }
            else if (crouched && !input.Movement.Crouch.IsPressed())
            {
                animator.SetBool("Crouch", false);
                col.offset = new Vector2(col.offset.x, 0);
                col.size = new Vector2(col.size.x, 4);
                crouched = false;
            }

            // Bieganie i chodzenie
            if (horizontal != 0)
            {
                if (cantMoveLeft && horizontal < 0)
                {

                    Debug.Log("Nie możesz ciągnąć krzesła");
                }
                else if (cantMoveRight && horizontal > 0)
                {
                    Debug.Log("Nie możesz ciągnąć krzesła");
                }
                else
                {
                    if (input.Movement.Run.IsPressed() && holdingObject == false)
                    {
                        animator.SetBool("Running", true);
                        rb.velocity = new Vector2(runningSpeed * horizontal, rb.velocity.y);
                    }
                    else
                    {
                        animator.SetBool("Running", false);
                        rb.transform.position += new Vector3(walkingSpeed * horizontal, 0, 0);

                    }
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

        // Metoda zabijająca postać gracza i wczytująca jego ostatni zapis lub zaczynająca od nowa grę, jeśli takiego nie ma
        public void KillPlayer(string killerName)
        {
            Debug.Log("You were killed by " + killerName);
            if (File.Exists(Application.persistentDataPath + "/save.wth"))
            {
                DO.PlayerData data = DO.SaveGame.LoadPlayer();
                //GameObject player = Instantiate(playerPrefab, new Vector3(data.position[0], data.position[1], data.position[2]), Quaternion.identity);

                StartCoroutine(RC.SceneChanger.MovePlayerToScene(data.levelId, gameObject, new Vector3(data.position[0], data.position[1], data.position[2]), new Vector3(data.cameraPosition[0], data.cameraPosition[1], data.cameraPosition[2])));
                //Destroy(gameObject);
            }
            else
            {
                //GameObject player = Instantiate(playerPrefab, new Vector3(0, -2.49f, 0), Quaternion.identity);

                StartCoroutine(RC.SceneChanger.MovePlayerToScene(3, gameObject, new Vector3(0, -2.49f, 0), new Vector3(0, 0, 0)));
                //Destroy(gameObject);
            }
            
        }
    }
}
 