using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invicibilityTimeAfterHit = 3f;
    public float invicibilityFlashDelay = 0.15f;
    public bool isInvicible = false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;

    public AudioClip hitSound;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of PlayerHealth in the scene");
            return;
        }
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    public void HealPlayer(int _amount)
    {
        if((currentHealth + _amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += _amount;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int _damage)
    {
        if(!isInvicible)
        {
            AudioManager.instance.PlayClipAt(hitSound, transform.position);
            currentHealth -= _damage;
            healthBar.SetHealth(currentHealth);

            if(currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvicible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());
            CameraShake.instance.ShakeIt(0.06f, 0.5f);
        }
    }

    public void Die()
    {
        PlayerMovement.instance.enabled = false; // Block character movements (disable PlayerMovement script)
        PlayerMovement.instance.animator.SetTrigger("Die"); // Play death animation
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic; // Prevent physical interactions between the other elements of the scene (Disable the physic of the character)
        PlayerMovement.instance.rb.velocity = Vector3.zero; // Reset the forces applied to the character
        PlayerMovement.instance.playerCollider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator InvicibilityFlash()
    {
        while(isInvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);

            if(PlayerElementSkills.instance.isIce)
            {
                graphics.color = ElementSystemUI.instance.iceColor;
            }
            else
            {
                graphics.color = ElementSystemUI.instance.fireColor;
            }
            
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        isInvicible = false;
    }
}
