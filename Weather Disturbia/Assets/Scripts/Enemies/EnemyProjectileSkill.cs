using UnityEngine;

public class EnemyProjectileSkill : MonoBehaviour
{
    public int damage;
    public float speedProjectile;
    public float timeBeforeAutoDestroy = 5; // Avoid to have too much object in the scene for 
    
    private Vector3 targetPlayerPosition;
    private Vector3 moveDir; // Direction of the projectile

    void Start()
    {
        targetPlayerPosition = PlayerMovement.instance.GetComponent<Transform>().position;
        transform.right = Vector3.Lerp(transform.up, (targetPlayerPosition - transform.position), 1);
        moveDir = (targetPlayerPosition - transform.position).normalized;

        Destroy(gameObject, timeBeforeAutoDestroy);
    }

    void Update()
    {
        transform.position += moveDir * speedProjectile * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // The player takes damages if the enemy projectile hits him
        if(collision.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
