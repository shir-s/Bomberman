using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    public int score = 0; // ניקוד השחקן
    public Text scoreText; // רכיב ה-UI שמציג את הניקוד
    public GameObject scorePopupPrefab; // פריפאב שמופיע עם 100 נקודות
    public Transform enemiesParent; // הורה של כל האויבים בסצנה

    private void Start()
    {
        UpdateScoreUI(); // עדכון התצוגה של הניקוד בהתחלה
    }

    // פונקציה שמוסיפה ניקוד לשחקן
    public void AddScore(int points, Vector3 position)
    {
        score += points;
        UpdateScoreUI();

        // הצגת התמונה הקטנה של 100
        if (scorePopupPrefab != null)
        {
            GameObject popup = Instantiate(scorePopupPrefab, position, Quaternion.identity);
            Destroy(popup, 1.5f); // השמדת התמונה לאחר זמן קצר
        }
    }

    // עדכון טקסט הניקוד
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // בדיקה אם כל האויבים נהרגו
    public bool AreAllEnemiesDefeated()
    {
        return enemiesParent != null && enemiesParent.childCount == 0;
    }

    // פונקציה לבדוק אם השחקן ניצח
    private void CheckWinCondition()
    {
        if (AreAllEnemiesDefeated())
        {
            Debug.Log("You Win!");
            // ניתן להוסיף כאן מעבר למסך ניצחון
        }
    }

    // קריאה לפונקציה כל פעם שאויב נהרג
    public void OnEnemyDefeated(Vector3 position)
    {
        AddScore(100, position);
        CheckWinCondition();
    }
}