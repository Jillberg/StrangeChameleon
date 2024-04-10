using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    Rigidbody2D rb;
    public float moveSpeed = 5f; //for messing around with the speed, should be changed later
    public float jumpPower=4f; //likewise
    bool isGrounded = false;
    bool isFacingRight = true;
    Animator animator;
    public InputAction playerControls;
    Vector2 moveDirection = Vector2.zero;
    

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void onDisEnable()
    {
        playerControls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerControls.ReadValue<Vector2>();
        //horizontalInput = Input.GetAxis("Horizontal"); old input system
        FlipSprite();
        if(Input.GetButtonDown("Jump")&&isGrounded)
        {
            rb.velocity=new Vector2(rb.velocity.x,jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }
    }

    private void FixedUpdate()
    {
        //rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        animator.SetFloat("xVelocity",Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private void FlipSprite()
    {
        if(isFacingRight&&moveDirection.x<0f|| !isFacingRight&&moveDirection.x>0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale= ls;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("wtf");
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
    }

    
}
