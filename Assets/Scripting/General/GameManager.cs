using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public int score = 0;
    public int timeRemaining = 200;
    public int livesRemaining = 3;
    public string stageSceneName = "StageScene"; // שם הסצנה שמופיעה לפני תחילת סבב חדש
    public string gameOverSceneName = "GameOverScene";
    public string gameSceneName = "GameScene";
    private bool isTimerRunning = false;
    
    private bool isTransitioning = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // הבטחה שהאובייקט לא יושמד בעת טעינת סצנה חדשה
    }
    
    private void Start()
    {
        if (livesRemaining == 0) livesRemaining = 3;
        UpdateUI();
        StartTimer();
        //InvokeRepeating(nameof(UpdateTimer), 1f, 1f);
    }
    
    private void UpdateUI()
    {
        UIManager.Instance.UpdateScoreText(score);
        UIManager.Instance.UpdateTimeText(timeRemaining);
        UIManager.Instance.UpdateLivesText(livesRemaining);
    }

    private void StartTimer()
    {
        if (isTimerRunning) return; // אם הטיימר כבר פועל, לא להפעיל שוב
        CancelInvoke(nameof(UpdateTimer)); // איפוס כל הטיימרים
        InvokeRepeating(nameof(UpdateTimer), 1f, 1f); // התחלת טיימר
        isTimerRunning = true;
    }
    
    private void StopTimer()
    {
        CancelInvoke(nameof(UpdateTimer)); // עצירת כל הטיימרים לפונקציה UpdateTimer
        isTimerRunning = false; // עדכון הדגל
    }
    
    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining--;
            UIManager.Instance.UpdateTimeText(timeRemaining);

            if (timeRemaining <= 0)
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
    
    public void LoseLife()
    {
        Debug.Log("LoseLife called!");
        if (isTransitioning) return;
        isTransitioning = true;
        livesRemaining--;
        UIManager.Instance.UpdateLivesText(livesRemaining);
        if (livesRemaining > 0)
        {
            TransitionToStageScene();
        }
        else
        {
            EndGame();
        }
    }
    
    private void TransitionToStageScene()
    {
        Debug.Log("Transitioning to StageScene...");
        SceneManager.LoadScene(stageSceneName);
        StartCoroutine(WaitBeforeTransitionToStageScene());
    }

    private System.Collections.IEnumerator WaitBeforeTransitionToStageScene()
    {
        yield return new WaitForSeconds(2f); // המתנה ל-2 שניות
        RestartGame();
    }
    
    private void RestartGame()
    {
        Debug.Log("Restarting Game...");
        StopTimer(); // עצירת טיימר ישן לפני הטעינה
        SceneManager.LoadScene(gameSceneName);
        isTransitioning = false;
        StartCoroutine(WaitForSceneLoad());
    }

    private System.Collections.IEnumerator WaitForSceneLoad()
    {
        // מחכה לסיום טעינת הסצנה
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == gameSceneName);

        Debug.Log("GameScene loaded. Updating UI...");
        UIManager.Instance.AssignUIComponents(); // משייכים מחדש את רכיבי ה-UI
        UIManager.Instance.UpdateTimeText(timeRemaining);
        UIManager.Instance.UpdateLivesText(livesRemaining);
        UIManager.Instance.UpdateScoreText(score);

        // הפעלת הטיימר מחדש
        StartTimer();
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");
        CancelInvoke(nameof(UpdateTimer)); // עצירת הטיימר
        SceneManager.LoadScene(gameOverSceneName); // טעינת סצנת הפסד
    }
}