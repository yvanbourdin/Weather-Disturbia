using UnityEngine;

public class EnemyProjectileSkill : MonoBehaviour
{
    public int damage;
    public float speedProjectile;
    public float timeBeforeAutoDestroy = 5;
    
    private Vector3 targetPlayerPosition;
    private Vector3 moveDiv;


    void Start()
    {
        targetPlayerPosition = PlayerMovement.instance.GetComponent<Transform>().position;
        transform.right = Vector3.Lerp(transform.up, (targetPlayerPosition - transform.position), 1);
        moveDiv = (targetPlayerPosition - transform.position).normalized;

        Destroy(gameObject, timeBeforeAutoDestroy);
    }

    void Update()
    {
        transform.position += moveDiv * speedProjectile * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
