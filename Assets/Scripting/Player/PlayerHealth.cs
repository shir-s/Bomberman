using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    private Animator animator;
    private BombController bombController;
    private PlayerController playerController;
    private Rigidbody2D rb; // Added
    
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bombController = GetComponent<BombController>();
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>(); // Added
    }
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            TakeDamage();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            TakeDamage();
        }
    }
    
    private void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            DeathSequence();
        }
    }
    
    //Befor adding 2 colliders:
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
    //     {
    //         currentHealth--;
    //         if (currentHealth <= 0)
    //         {
    //             DeathSequence();
    //         }
    //     }
    // }
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //     {
    //         currentHealth--;
    //         if (currentHealth <= 0)
    //         {
    //             DeathSequence();
    //         }
    //     }
    // }

    private void DeathSequence()
    {
        playerController.enabled = false;
        //bombController.enabled = false;
        animator.speed = 1f;
        bombController.enabled = true;
        rb.linearVelocity = Vector2.zero; // Added
        SoundManager.Instance.PlayDeathSound();
        animator.SetTrigger("IsDead");
        
        Debug.Log("Calling GameManager.Instance.LoseLife()");
        GameManager.Instance.LoseLife();
        //GameManager.Instance.RestartGame();
        //animator.SetBool("IsDead", true);
        //Invoke(nameof(OnDeathSequenceEnd), 1.25f);
    }

    private void OnDeathSequenceEnd()
    {
        gameObject.SetActive(false);
    }
    
    
    // public void RestoreHealth()
    // {
    //     currentHealth = maxHealth;
    //     Debug.Log($"Player Health Restored: {currentHealth}");
    // }
    
    
}