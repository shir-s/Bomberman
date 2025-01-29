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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // הורס אובייקט קיים נוסף
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (livesRemaining == 0) livesRemaining = 3;
        UpdateUI();
        StartTimer();
    }

    private void UpdateUI()
    {
        UIManager.Instance.UpdateScoreText(score);
        UIManager.Instance.UpdateTimeText(timeRemaining);
        UIManager.Instance.UpdateLivesText(livesRemaining);
    }
    
    private void StartTimer()
    {
        StopTimer();
        if (isTimerRunning) return;
        isTimerRunning = true;
        InvokeRepeating(nameof(UpdateTimer), 1f, 1f);
        Debug.Log("Timer started.");
    }


    private void StopTimer()
    {
        CancelInvoke(nameof(UpdateTimer));
        isTimerRunning = false;
        Debug.Log("Timer stopped.");
    }

    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining--;
            UIManager.Instance.UpdateTimeText(timeRemaining);

            if (timeRemaining <= 0)
            {
                LoseLife();
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
        StopTimer();

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
        StopTimer();
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
        StopTimer();
        isTimerRunning = false;
        isTransitioning = false;
        timeRemaining = 200;
        SceneManager.LoadScene(gameSceneName);
        StartCoroutine(WaitForSceneLoad());
    }

    private System.Collections.IEnumerator WaitForSceneLoad()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == gameSceneName);

        Debug.Log("GameScene loaded. Initializing...");
        UIManager.Instance.AssignUIComponents();
        UpdateUI();
        StartTimer();
    }
    
    public bool AreAllEnemiesDefeated()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");
        StopTimer();
        SceneManager.LoadScene(gameOverSceneName);
    }
    
    public void WinGame()
    {
        Debug.Log("Player won the game! Loading Win Scene...");
        SceneManager.LoadScene("WinScene");
    }

}