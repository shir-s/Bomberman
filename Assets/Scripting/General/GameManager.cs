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

    public Transform playerStartPosition; // נקודת התחלה של השחקן
    public GameObject player; // השחקן עצמו
    
    private bool isTransitioning = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // הבטחה שהאובייקט לא יושמד בעת טעינת סצנה חדשה
    }
    
    private void Start()
    {
        UpdateUI();
        StartTimer();
        //InvokeRepeating(nameof(UpdateTimer), 1f, 1f);
    }
    
    private void StartTimer()
    {
        CancelInvoke(nameof(UpdateTimer)); // איפוס הטיימר במעבר בין סצנות
        InvokeRepeating(nameof(UpdateTimer), 1f, 1f);
    }
    
    private void UpdateUI()
    {
        UIManager.Instance.UpdateScoreText(score);
        UIManager.Instance.UpdateTimeText(timeRemaining);
        UIManager.Instance.UpdateLivesText(livesRemaining);
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

        if (livesRemaining > 0)
        {
            TransitionToStageScene();
        }
        else
        {
            EndGame();
        }

        UIManager.Instance.UpdateLivesText(livesRemaining);
    }
    
    private void TransitionToStageScene()
    {
        Debug.Log("Transitioning to StageScene...");
        SceneManager.LoadScene(stageSceneName); // טעינת סצנת StageScene
        StartCoroutine(WaitAndRestartGame());
    }
    
    private System.Collections.IEnumerator WaitAndRestartGame()
    {
        yield return new WaitForSeconds(3f); // המתנה ל-3 שניות
        ResetGame();
    }
    
    private void ResetGame()
    {
        Debug.Log("Restarting Game...");
        SceneManager.LoadScene(gameSceneName); // טעינת סצנת המשחק
        timeRemaining = 200; // איפוס הזמן
        UIManager.Instance.UpdateTimeText(timeRemaining);
        isTransitioning = false;
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");
        CancelInvoke(nameof(UpdateTimer)); // עצירת הטיימר
        SceneManager.LoadScene(gameOverSceneName); // טעינת סצנת הפסד
    }
}