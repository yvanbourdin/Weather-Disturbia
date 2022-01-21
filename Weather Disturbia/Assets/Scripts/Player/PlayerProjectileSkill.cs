using UnityEngine;

public class PlayerProjectileSkill : MonoBehaviour
{
    public int damage;
    public float speedProjectile;
    public float timeBeforeAutoDestroy = 3;
    public bool isDestructible = true;
    public bool canDestroyOtherProjectiles = false;

    private Vector3 movement;

    void Start()
    {
        // Change the direction of the projectile depending on the player orientation
        if (PlayerMovement.instance.spriteRenderer.flipX)
        {
            speedProjectile = -speedProjectile;
        }
        
        Destroy(gameObject, timeBeforeAutoDestroy);
    }

    void Update()
    {
        // Projectile movement
        movement = Vector3.right * speedProjectile * Time.deltaTime;
        transform.position += movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy the projectile if it hits an obstacle
        if (collision.CompareTag("Ground") || collision.CompareTag("Enemy"))
        {
            if (isDestructible)
            {
                Destroy(gameObject);
            }            
        }

        // Destroy the enemy projectiles if they hit the player's projectile
        if (collision.CompareTag("EnemyProjectile") && canDestroyOtherProjectiles)
        {
            Destroy(collision.gameObject);
        }
    }
}
