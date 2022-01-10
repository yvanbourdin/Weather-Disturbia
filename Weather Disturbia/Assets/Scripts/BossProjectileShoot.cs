using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileShoot : MonoBehaviour
{
    public GameObject bossProjectilePrefab1;
    public GameObject bossProjectilePrefab2;
    public float bossProjectileCooldownMax;

    private bool playerIsInRange = false;
    private bool projectileSkillOnCooldown = false;

    void Update()
    {
        if (playerIsInRange && !projectileSkillOnCooldown)
        {
            ShootProjectile();
        }
    }

    public void ShootProjectile()
    {
        if (Random.value < 0.5f) // 1st skill
        {
            StartCoroutine(BurstProjectiles());
        }
        else // 2nd skill
        {
            Instantiate(bossProjectilePrefab2, gameObject.transform.position, gameObject.transform.rotation);
        }

        StartCoroutine(CooldownShootProjectile());
    }

    private IEnumerator BurstProjectiles()
    {
        int nbProjectiles = 3;

        for (int i = 0; i < nbProjectiles; i++)
        {
            Instantiate(bossProjectilePrefab1, gameObject.transform.position, gameObject.transform.rotation);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator CooldownShootProjectile()
    {
        projectileSkillOnCooldown = true;
        float timeLeft = bossProjectileCooldownMax;

        while (timeLeft >= 0.01f)
        {
            timeLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        projectileSkillOnCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
