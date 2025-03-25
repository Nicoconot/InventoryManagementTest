using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Components
    private Animator controller;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    //Animation params
    int movementDirection = 0;

    //Movement
    [SerializeField] float speed = 5;

    protected float horizontal, vertical;
    protected bool isMoving = false;

    void Awake()
    {
        controller = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }


    protected virtual void GetMovement()
    {
        //Set the horizontal and vertical movement parameters here
    }

    void FixedUpdate()
    {
        ApplyMovement();

        ApplyAnimations();
        controller.SetInteger("walking", movementDirection);
    }

    void ApplyMovement()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    void ApplyAnimations()
    {
        if(!isMoving) 
        {
            movementDirection = 0;
            return;
        }
        float hVelocity = rb.velocity.x;
        float vVelocity = rb.velocity.y;
        float hVelocityAbs = Mathf.Abs(hVelocity);
        float vVelocityAbs = Mathf.Abs(vVelocity);

        if(hVelocityAbs > vVelocityAbs) 
        {
            movementDirection = 1;
            sr.flipX = hVelocity < 0;
        }
        else 
        {
            movementDirection = vVelocity > 0 ? 2 : 3;
        }
    }
}
