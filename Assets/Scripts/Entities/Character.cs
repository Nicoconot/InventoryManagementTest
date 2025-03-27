using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Components
    protected Animator controller;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    //Animation params
    private int movementDirection = 0;

    //Movement
    [SerializeField] float speed = 5;

    protected float horizontal, vertical;
    protected bool isMoving = false;
    protected bool isDead = false;
    protected bool canMove = true;

    //Stats
    protected int maxHp = 20;
    protected int currentHp = 20;
    protected int currentCoins = 0;

    public int CurrentHp { get => currentHp; private set => currentHp = value; }
    public int Coins { get => currentCoins; private set => currentCoins = value; }

    protected virtual void Awake()
    {
        controller = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void SetHP(int amount)
    {
        currentHp = amount;
    }

    public virtual void SetCoins(int amount)
    {
        currentCoins = amount;
    }

    public virtual void TakeDamage(int dmgAmount)
    {
        currentHp -= dmgAmount;

        if (currentHp <= 0)
        {
            //dead
            controller.SetBool("dead", true);
            isDead = true;
            canMove = false;
        }
    }

    public virtual void RecoverHealth(int amount)
    {
        currentHp += amount;

        Mathf.Clamp(currentHp, 0, maxHp);
    }

    public virtual void Attack()
    {
        controller.SetTrigger("attack");
    }

    protected virtual void GetMovement()
    {
        //Set the horizontal and vertical movement parameters here
    }

    private void FixedUpdate()
    {
        ApplyMovement();

        ApplyAnimations();
        controller.SetInteger("walking", movementDirection);
    }

    private void ApplyMovement()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
    }

    private void ApplyAnimations()
    {
        if (!isMoving)
        {
            movementDirection = 0;
            return;
        }
        float hVelocity = rb.linearVelocity.x;
        float vVelocity = rb.linearVelocity.y;
        float hVelocityAbs = Mathf.Abs(hVelocity);
        float vVelocityAbs = Mathf.Abs(vVelocity);

        if (hVelocityAbs > vVelocityAbs)
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
