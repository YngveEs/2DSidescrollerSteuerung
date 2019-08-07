using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(Animator))]
public class MovementScript : MonoBehaviour
{
    [Header("Movement Variables")]
    public float walkSpeed = 5f;
    public float runSpeed = 20f;
    public float jumpSpeed = 5f;


    private Rigidbody2D rigidB;
    private Animator anim;
    private GroundChecker groundCheck;


    void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = GetComponent<GroundChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0.01f || Input.GetAxis("Horizontal") > 0.01f)
        {
            if (groundCheck.IsGrounded())
                Walk();
            else
                Fall();
        }
        if (Input.GetButtonDown("Jump") && groundCheck.IsGrounded())
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
    }
    void Walk()
    {
        rigidB.velocity = new Vector2(Input.GetAxis("Horizontal") * walkSpeed, rigidB.velocity.y);
    }

    void Run()
    {
        rigidB.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, rigidB.velocity.y);

    }

    void Jump()
    {
        rigidB.velocity = new Vector2(rigidB.velocity.x, jumpSpeed);

    }
    void Fall()
    {
        print("Falling!");
    }
}
