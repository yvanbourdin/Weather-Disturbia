using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    [Range(0, 100)]
    public int percentageDropHealPowerUp;

    public GameObject healPowerUpDrop;
    public GameObject objectToDestroy; // destroy the enemy when it dies
    public AudioClip killSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Apply damages to the enemy if player's projectiles hit
        if (collision.CompareTag("PlayerProjectile"))
        {
            int damage = collision.gameObject.GetComponent<PlayerProjectileSkill>().damage;
            ApplyDamage(damage);
        }
    }

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

        // Drop
        if (Random.value <= percentageDropHealPowerUp / 100f)
        {
            Instantiate(healPowerUpDrop, gameObject.transform.position, gameObject.transform.rotation);
        }
            
        Destroy(gameObject.transform.parent.gameObject);
    }
}
