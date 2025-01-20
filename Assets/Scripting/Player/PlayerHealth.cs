using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        Debug.Log($"Player Health: {currentHealth}");
    }
    
    public void RestoreHealth()
    {
        currentHealth = maxHealth;
        Debug.Log($"Player Health Restored: {currentHealth}");
    }
    
    private void Die()
    {
        Debug.Log("Player Died!");
        // Destroy(gameObject);
    }
    
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}