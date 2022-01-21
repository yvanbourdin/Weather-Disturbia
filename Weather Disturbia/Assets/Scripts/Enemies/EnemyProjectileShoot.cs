using System.Collections;
using UnityEngine;

public class EnemyProjectileShoot : MonoBehaviour
{
    public GameObject ennemyProjectilePrefab;
    public float ennemyProjectileCooldownMax;
    public SpriteRenderer spriteRenderer; // enemy's sprite

    private bool playerIsInRange = false;
    private bool projectileSKillOnCooldown = false;

    void Update()
    {
        if(playerIsInRange && !projectileSKillOnCooldown)
        {
            ShootProjectile();
        }

        Flip();
    }

    public void ShootProjectile()
    {
        Instantiate(ennemyProjectilePrefab, gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine(CooldownShootProjectile());
    }

    private void Flip()
    {
        // Change the direction of the enemy to launch its projectile depending on the player orientation
        if(PlayerMovement.instance.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    // Delay between 2 projectile shots
    IEnumerator CooldownShootProjectile()
    {
        projectileSKillOnCooldown = true;
        float timeLeft = ennemyProjectileCooldownMax; // time before another projectile can be launched 

        while(timeLeft >= 0.01f)
        {
            timeLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        projectileSKillOnCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When the player is in range of the enemy
        if (collision.CompareTag("Player"))
        {
            playerIsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // When the player is no longer in range of the enemy
        if (collision.CompareTag("Player"))
        {
            playerIsInRange = false;
        }
    }
}
