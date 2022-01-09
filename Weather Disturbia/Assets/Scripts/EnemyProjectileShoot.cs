using System.Collections;
using UnityEngine;

public class EnemyProjectileShoot : MonoBehaviour
{
    public GameObject ennemyProjectilePrefab;
    public float ennemyProjectileCooldownMax;
    public SpriteRenderer spriteRenderer;

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
        // Change the direction of the projectile depending on the player orientation
        if(PlayerMovement.instance.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    IEnumerator CooldownShootProjectile()
    {
        projectileSKillOnCooldown = true;
        float timeLeft = ennemyProjectileCooldownMax;

        while(timeLeft >= 0.01f)
        {
            timeLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        projectileSKillOnCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerIsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsInRange = false;
        }
    }
}
