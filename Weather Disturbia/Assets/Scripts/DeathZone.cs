using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    public int damageDeathZone = 10;

    private Animator fadeSystem;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision));
        }
    }

    private IEnumerator ReplacePlayer(Collider2D _collision)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        PlayerHealth.instance.TakeDamage(damageDeathZone);
        _collision.transform.position = CurrentSceneManager.instance.respawnPoint;
    }
}
