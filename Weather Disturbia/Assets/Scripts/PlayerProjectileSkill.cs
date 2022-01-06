using UnityEngine;

public class PlayerProjectileSkill : MonoBehaviour
{
    public int damage;
    public float speedProjectile;
    public float timeBeforeAutoDestroy = 3;

    public Rigidbody2D rb;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    void Start()
    {
        // Change the direction of the projectile depending on the character orientation
        if (PlayerMovement.instance.spriteRenderer.flipX)
        {
            speedProjectile = -speedProjectile;
        }
        
        Destroy(gameObject, timeBeforeAutoDestroy);
    }

    void Update()
    {
        horizontalMovement = speedProjectile * Time.deltaTime;
    }

    void FixedUpdate()
    {
        MoveProjectile(horizontalMovement);
    }

    void MoveProjectile(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
