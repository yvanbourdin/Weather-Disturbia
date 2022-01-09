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
        Destroy(gameObject.transform.parent.gameObject);
    }
}
