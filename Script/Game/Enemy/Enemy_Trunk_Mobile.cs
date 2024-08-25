using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Trunk_Mobile : Enemy
{
    [Header("Trunk details")]
    [SerializeField] private Trunk_Bullet bulletPrefab;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private float bulletSpeed = 7;
    [SerializeField] private float attackCooldown = 1.5f;
    private float lastTimeAttacked;

    protected override void Update()
    {
        base.Update();

        if (isDead)
            return;

        bool canAttack = Time.time > lastTimeAttacked + attackCooldown;

        if (isPlayerDetected && canAttack)
            Attack();


        HandleMovement();

        if (isGrounded)
            HandleTurnAround();
    }

    private void Attack()
    {
        idleTimer = idleDuration + attackCooldown;
        lastTimeAttacked = Time.time;
        ani.SetTrigger("attack");
    }

    private void CreateBullet()
    {
        Trunk_Bullet newBullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);

        Vector2 bulletVelocity = new Vector2(facingDir * bulletSpeed, 0);
        newBullet.SetVelocity(bulletVelocity);

        if (facingDir == 1)
            newBullet.FlipSprite();

        Destroy(newBullet.gameObject, 10);
    }

    private void HandleTurnAround()
    {
        if (!isGroundFronted || isWalled)
        {
            Flip();
            idleTimer = idleDuration;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void HandleMovement()
    {
        if (idleTimer > 0)
            return;

        rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocity.y);
    }
}
