using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    private Animator animator;
    private bool isDead = false;
    
    [Header("Score Popup")]
    [SerializeField] private GameObject scorePopupPrefab;
    [SerializeField] private int scoreValue = 100;
    
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
        if (isDead) return;
        
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
        isDead = true;
        EnemyMovement movement = GetComponent<EnemyMovement>();
        if (movement != null)
        {
            movement.StopMovement();
        }
        GameManager.Instance.AddScore(scoreValue);
        animator.SetTrigger("IsDead");
        CreateScorePopup();
        Invoke(nameof(OnDeathSequenceEnd), 1.1f);
    }

    private void OnDeathSequenceEnd()
    {
        gameObject.SetActive(false);
    }
    
    private void CreateScorePopup()
    {
        if (scorePopupPrefab != null)
        {
            Instantiate(scorePopupPrefab, transform.position, Quaternion.identity);
        }
    }
}
