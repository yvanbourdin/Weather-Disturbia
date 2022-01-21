using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int currentHealth;
    public int damageOnCollision = 30;

    public GameObject door; // acceed to the next level
    public GameObject objectToDestroy;
    public AudioClip killSound;
    public GameObject bossDrop;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Apply damages to the boss if player's projectiles hit
        if (collision.CompareTag("PlayerProjectile"))
        {
            int damage = collision.gameObject.GetComponent<PlayerProjectileSkill>().damage;
            ApplyDamage(damage);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Apply damages to the player if hits the boss
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }

    // When the boss takes damages
    public void ApplyDamage(int _damage)
    {
        currentHealth -= _damage;
        CameraShake.instance.ShakeIt(0.02f, 0.3f);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }
    }

    private void Die()
    {
        AudioManager.instance.PlayClipAt(killSound, transform.position);
        Instantiate(bossDrop, gameObject.transform.position, gameObject.transform.rotation); // heal power up drop
        door.SetActive(true); // get access to the next level
        Destroy(gameObject.transform.parent.gameObject);
    }
}
