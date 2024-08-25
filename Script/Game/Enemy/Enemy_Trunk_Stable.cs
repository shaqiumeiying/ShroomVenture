using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Trunk_Stable : Enemy
{
    [Header("Trunk Details")]
    [SerializeField] private Trunk_Bullet bulletPrefab;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private float bulletSpeed = 7;
    [SerializeField] private float attackCooldown = 1.5f;
    private float lastTimeAttack;

    protected override void Update()
    {
        base.Update();

        bool canAttack = Time.time > lastTimeAttack + attackCooldown;
        
        if (isPlayerDetected && canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        lastTimeAttack = Time.time;
        ani.SetTrigger("attack");
    }

    protected override void HandleAnimator()
    {}

    private void CreatBullet()
    {
        Trunk_Bullet newBullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);

        Vector2 BulletVelocity = new Vector2(facingDir * bulletSpeed, 0);
        newBullet.SetVelocity(BulletVelocity);
        Destroy(newBullet.gameObject, 10);
    }
}
