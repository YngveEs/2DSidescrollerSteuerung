using System;
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

    [Header("Check Status")]
    public bool running;
    public bool jumping;


    private Rigidbody2D rigidB;
    private Animator anim;
    private GroundChecker groundCheck;

    public bool FacingRight { get; private set; } = true;

    void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = GetComponent<GroundChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();
        if (Input.GetAxis("Horizontal") < 0.01f || Input.GetAxis("Horizontal") > 0.01f)
        {
            if (groundCheck.IsGrounded())
                Walk();
        }
        if (Input.GetButtonDown("Jump") && groundCheck.IsGrounded())
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Run();
            running = true;
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            running = false;
        }
    }


    void Walk()
    {
        rigidB.velocity = new Vector2(Input.GetAxis("Horizontal") * walkSpeed, rigidB.velocity.y);

        if ((FacingRight && Input.GetAxis("Horizontal") < 0) || (!FacingRight && Input.GetAxis("Horizontal") > 0))
        {
            Flip();
        }
    }

    void Run()
    {
        rigidB.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, rigidB.velocity.y);

        if ((FacingRight && Input.GetAxis("Horizontal") < 0) || (!FacingRight && Input.GetAxis("Horizontal") > 0))
        {
            Flip();
        }
    }

    void Jump()
    {
        rigidB.velocity = new Vector2(rigidB.velocity.x, jumpSpeed);

    }
    public void Flip()
    {
        gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, new Vector3(-1, 1, 1));
        FacingRight = !FacingRight;
    }


    private void UpdateAnimator()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rigidB.velocity.x));
        anim.SetFloat("velocityY", rigidB.velocity.y);

        anim.SetBool("running", running);
    }
}
