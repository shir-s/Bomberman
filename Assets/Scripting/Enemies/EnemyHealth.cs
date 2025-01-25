using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("IsDead");
        Invoke(nameof(OnDeathSequenceEnd), 1f);
    }

    private void OnDeathSequenceEnd()
    {
        gameObject.SetActive(false);
    }
}
