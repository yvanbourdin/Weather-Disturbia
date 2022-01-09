using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;

    public GameObject objectToDestroy;
    public AudioClip killSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            int damage = collision.gameObject.GetComponent<PlayerProjectileSkill>().damage;
            ApplyDamage(damage);
        }
    }

    public void ApplyDamage(int _damage)
    {
        currentHealth -= _damage;

        if(currentHealth <= 0)
        {
            Die();
            return;
        }
    }

    private void Die()
    {
        AudioManager.instance.PlayClipAt(killSound, transform.position);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
