using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bunny : Enemy
{
    [Header("Bunny Details")]
    [SerializeField] private float aggroDuration;
    private float aggroTimer;
    [SerializeField] private bool playerDetected;
    [SerializeField] private float detectionRange;
    private bool canFlip = true;

    protected override void Update()
    {
        base.Update();

        ani.SetFloat("xVelocity", rb.linearVelocity.x);
        aggroTimer -= Time.deltaTime;

        if (isDead)
            return;

        if(playerDetected)
            {
                canMove = true;
                aggroTimer = aggroDuration;
            }

        if(aggroTimer < 0)
        {
            canMove = false;
        }
        HandleCollision();
        HandleMovement();

        if (isGrounded)
            HandleTurnAround();
    }

    private void HandleTurnAround()
    {
        if (!isGroundFronted || isWalled)
        {
            Flip();
            canMove = false;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void HandleMovement()
    {
        if (canMove == false)
            return;

        HandleFlip(piggy.transform.position.x);

        rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocity.y);
    }

    protected override void HandleFlip(float xValue)
    {
         if (xValue < transform.position.x && facingRight || xValue > transform.position.x && !facingRight)
           {
                if(canFlip)
                    {
                        canFlip = false;
                        Invoke(nameof(Flip),.3f);
                    }
           } 
    }

    protected override void Flip()
    {
        base.Flip();
        canFlip = true;

    }

    protected override void HandleCollision()
    {
        base.HandleCollision();

        playerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, detectionRange, whatIsPlayer);
    }



}
