using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int score = 0; // ניקוד השחקן
    public int timeRemaining = 200; // הזמן שנותר
    public int livesRemaining = 3; // מספר הסבבים (LEFT)

    public Transform playerStartPosition; // נקודת התחלה של השחקן
    public GameObject player; // השחקן עצמו

    private void Start()
    {
        // עדכון ה-UI בתחילת המשחק
        UIManager.Instance.UpdateScoreText(score);
        UIManager.Instance.UpdateTimeText(timeRemaining);
        UIManager.Instance.UpdateLivesText(livesRemaining);

        // הפעלת טיימר
        InvokeRepeating(nameof(UpdateTimer), 1f, 1f);
    }

    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining--;
            UIManager.Instance.UpdateTimeText(timeRemaining);

            if (timeRemaining == 0)
            {
                LoseLife(); // הפסד סבב עקב נגמר הזמן
            }
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UIManager.Instance.UpdateScoreText(score);
    }

    public void OnPlayerDeath()
    {
        LoseLife(); // הפסד סבב עקב מוות
    }

    private void LoseLife()
    {
        livesRemaining--;

        if (livesRemaining > 0)
        {
            ResetGame(); // איפוס המשחק אם נותרו חיים
        }
        else
        {
            EndGame(); // הפסד סופי
        }

        UIManager.Instance.UpdateLivesText(livesRemaining);
    }

    private void ResetGame()
    {
        Debug.Log("Resetting game...");
        timeRemaining = 200; // איפוס הטיימר
        UIManager.Instance.UpdateTimeText(timeRemaining);

        // הזזת השחקן לנקודת ההתחלה
        player.transform.position = playerStartPosition.position;

        // ניתן להוסיף כאן איפוס מצבים נוספים
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");
        CancelInvoke(nameof(UpdateTimer)); // עצירת הטיימר
        // ניתן להוסיף כאן מעבר למסך הפסד
    }
}