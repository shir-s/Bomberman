using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    private Animator animator;
    
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
        GameManager.Instance.AddScore(100);
        animator.SetTrigger("IsDead");
        CreateScorePopup();
        Invoke(nameof(OnDeathSequenceEnd), 1f);
    }

    private void OnDeathSequenceEnd()
    {
        gameObject.SetActive(false);
    }
    
    private void CreateScorePopup()
    {
        if (scorePopupPrefab != null)
        {
            // יצירת ה-ScorePopup במיקום של האויב
            Instantiate(scorePopupPrefab, transform.position, Quaternion.identity);
        }
    }
}
