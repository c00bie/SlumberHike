using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private NewInput input;

    void Awake()
    {
        input = new NewInput();
        spriteRenderer = GetComponent<SpriteRenderer>();
        input.Enable();
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float horizontal = input.Movement.Horizontal.ReadValue<float>();
        if (horizontal != 0)
            spriteRenderer.flipX = horizontal > 0;
    }
}
