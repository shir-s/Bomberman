using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public int score = 0;
    public int timeRemaining = 200;
    public int livesRemaining = 3;
    public string stageSceneName = "StageScene";
    public string gameOverSceneName = "GameOverScene";
    public string gameSceneName = "GameScene";

    private bool isTimerRunning = false;
    private bool isTransitioning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
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
        StartCoroutine(WaitBeforeTransitionToStageScene());
    }

    private System.Collections.IEnumerator WaitBeforeTransitionToStageScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(stageSceneName);
        StartCoroutine(WaitForStageSceneLoad());
    }

    private System.Collections.IEnumerator WaitForStageSceneLoad()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == stageSceneName);
        Debug.Log("StageScene loaded. Waiting for 3 seconds...");
        yield return new WaitForSeconds(3f);
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
    
    public void RestartFullGame()
    {
        Debug.Log("Restarting full game...");
        StopTimer();
        
        score = 0;
        livesRemaining = 3;
        timeRemaining = 200;

        SceneManager.LoadScene("OpenScene");

        StartCoroutine(WaitForGameSceneLoad());
    }

    
    private System.Collections.IEnumerator WaitForGameSceneLoad()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "GameScene");

        Debug.Log("GameScene loaded. Initializing UI and timer...");

        if (UIManager.Instance != null)
        {
            yield return new WaitForSeconds(0.5f); 
            UIManager.Instance.AssignUIComponents();

            if (UIManager.Instance.timeText != null &&
                UIManager.Instance.scoreText != null &&
                UIManager.Instance.livesText != null)
            {
                UIManager.Instance.UpdateTimeText(timeRemaining);
                UIManager.Instance.UpdateLivesText(livesRemaining);
                UIManager.Instance.UpdateScoreText(score);
                StartTimer();
            }
            else
            {
                Debug.LogError("UI components are still missing after assignment!");
            }
        }
        else
        {
            Debug.LogError("UIManager instance is null!");
        }
    }



}