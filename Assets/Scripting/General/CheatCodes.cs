using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCodes : MonoBehaviour
{
    [SerializeField] private Transform playerStartPosition; // נקודת התחלה של השחקן
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Cheat Code Activated: Return to Start Position");
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player != null && playerStartPosition != null)
            {
                player.position = playerStartPosition.position;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Cheat Code Activated: Transition to GameOver Scene");
            SceneManager.LoadScene("GameOverScene");
        }


        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Cheat Code Activated: Infinite Lives");

            // מציאת השחקן ועדכון ה-PlayerHealth
            PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.currentHealth = 999; // הגדרת חיים אינסופיים
                Debug.Log("Player health set to infinite!");
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on Player!");
            }
        }

    }
}