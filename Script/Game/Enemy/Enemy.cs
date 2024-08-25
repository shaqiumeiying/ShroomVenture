using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr => GetComponent<SpriteRenderer>();
    protected Transform piggy;
    protected Animator ani;
    protected Rigidbody2D rb;
    protected Collider2D[] colliders;
    protected bool canMove;

    [Header("General info")]
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float idleDuration = 1.5f;
    protected float idleTimer;

    [Header("Death details")]
    [SerializeField] protected float deathImpactSpeed = 3;
    protected bool isDead;


    [Header("Basic collision")]
    [SerializeField] protected float groundCheckDistance = 1.1f;
    [SerializeField] protected float wallCheckDistance = .7f;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float playerDetectionDistance = 15;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform groundCheck;
    protected bool isPlayerDetected;
    protected bool isGrounded;
    protected bool isWalled;
    protected bool isGroundFronted;

    protected int facingDir = -1;
    protected bool facingRight = false;


    protected virtual void Awake()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colliders = GetComponentsInChildren<Collider2D>();
    }

    protected virtual void Start()
    {
        InvokeRepeating(nameof(UpdatePlayerRef), 0, 1);
        if (sr.flipX == true && !facingRight)
        {
            sr.flipX = false;
            Flip();
        }
    }

    private void UpdatePlayerRef()
{
    if (GameManager.instance != null && GameManager.instance.piggy != null)
    {
        piggy = GameManager.instance.piggy.transform;
    }
    else
    {
        piggy = null;
    }
}


    protected virtual void Update()
    {
        HandleCollision();
        HandleAnimator();

        idleTimer -= Time.deltaTime;

    }

    public virtual void Die()
    {
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        ani.SetTrigger("hit");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, deathImpactSpeed);
        rb.gravityScale = 3f;
        isDead = true;

        Destroy(gameObject, 10);
    }

    protected virtual void HandleFlip(float xValue)
    {
        if (isDead) return;
        if (xValue < transform.position.x && facingRight || xValue > transform.position.x && !facingRight)
            Flip();
    }

    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }


    [ContextMenu("Change Facing Direction")]
    public void FlipDefaultFacingDireciton()
    {
        sr.flipX = !sr.flipX;
    }

    protected virtual void HandleAnimator()
    {
        ani.SetFloat("xVelocity", rb.linearVelocity.x);
    }

    protected virtual void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isGroundFronted = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWalled = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, playerDetectionDistance, whatIsPlayer);
    }

    protected virtual void OnDrawGizmos()
    {

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (wallCheckDistance * facingDir), transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (playerDetectionDistance * facingDir), transform.position.y));
    }
}
