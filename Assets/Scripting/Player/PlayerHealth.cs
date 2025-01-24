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
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                DeathSequence();
            }
        }
    }

    private void DeathSequence()
    {
        playerController.enabled = false;
        bombController.enabled = false;
        rb.linearVelocity = Vector2.zero; // Added
        animator.SetTrigger("IsDead");
        Invoke(nameof(OnDeathSequenceEnd), 1.25f);
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