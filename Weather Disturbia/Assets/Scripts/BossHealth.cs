using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int currentHealth;

    public GameObject door;
    public GameObject objectToDestroy;
    public AudioClip killSound;
    public GameObject bossDrop;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerProjectile"))
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
        Instantiate(bossDrop, gameObject.transform.position, gameObject.transform.rotation);
        door.SetActive(true);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
