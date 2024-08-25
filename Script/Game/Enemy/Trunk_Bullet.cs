using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk_Bullet : MonoBehaviour
{
    [SerializeField] private string playerLayerName = "Player";
    [SerializeField] private string groundLayerName = "Ground";
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void FlipSprite() => sr.flipX = !sr.flipX;

    public void SetVelocity(Vector2 velocity) => rb.linearVelocity = velocity;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer(playerLayerName))
        {
            collision.GetComponent<Piggy>().Knockback(transform.position.x);
            Destroy(gameObject);
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
        {
            Destroy(gameObject, 0.5f);
        }

    }
}
