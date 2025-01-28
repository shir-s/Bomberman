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
        if (livesRemaining == 0) livesRemaining = 3; // איפוס כמות החיים בתחילת המשחק
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
        if (isTimerRunning) return; // אם הטיימר כבר פועל, לא להפעיל שוב
        CancelInvoke(nameof(UpdateTimer)); // איפוס כל הטיימרים
        isTimerRunning = true; // דגל להתחלת טיימר
        InvokeRepeating(nameof(UpdateTimer), 1f, 1f); // התחלת טיימר
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
            UIManager.Instance.UpdateTimeText(timeRemaining); // עדכון ה-UI

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
        StopTimer(); // עצירת הטיימר הנוכחי

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
        SceneManager.LoadScene(gameSceneName); // טעינת סצנת המשחק
        isTransitioning = false; // איפוס דגל המעבר

        // בדיקה שה-UIManager מתעדכן מחדש
        StartCoroutine(WaitForSceneLoad());
    }

    private System.Collections.IEnumerator WaitForSceneLoad()
    {
        // מחכה לטעינת הסצנה
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == gameSceneName);

        Debug.Log("GameScene loaded. Initializing game state...");
        timeRemaining = 200; // איפוס הזמן
        UIManager.Instance.AssignUIComponents(); // משייך מחדש את רכיבי ה-UI
        UpdateUI(); // עדכון ה-UI עם הנתונים הנוכחיים
        StartTimer(); // הפעלת הטיימר מחדש
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");
        StopTimer(); // עצירת הטיימר
        SceneManager.LoadScene(gameOverSceneName); // טעינת סצנת הפסד
    }
}




// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// public class GameManager : MonoSingleton<GameManager>
// {
//     public int score = 0;
//     public int timeRemaining = 200;
//     public int livesRemaining = 3;
//     public string stageSceneName = "StageScene";
//     public string gameOverSceneName = "GameOverScene";
//     public string gameSceneName = "GameScene";
//     private bool isTimerRunning = false;
//     
//     private bool isTransitioning = false;
//
//     private void Awake()
//     {
//         DontDestroyOnLoad(gameObject);
//     }
//     
//     private void Start()
//     {
//         if (livesRemaining == 0) livesRemaining = 3;
//         UpdateUI();
//         StartTimer();
//     }
//     
//     private void UpdateUI()
//     {
//         UIManager.Instance.UpdateScoreText(score);
//         UIManager.Instance.UpdateTimeText(timeRemaining);
//         UIManager.Instance.UpdateLivesText(livesRemaining);
//     }
//
//     private void StartTimer()
//     {
//         if (isTimerRunning) return; 
//         CancelInvoke(nameof(UpdateTimer));
//         InvokeRepeating(nameof(UpdateTimer), 1f, 1f);
//         isTimerRunning = true;
//     }
//     
//     private void StopTimer()
//     {
//         CancelInvoke(nameof(UpdateTimer));
//         isTimerRunning = false;
//     }
//     
//     private void UpdateTimer()
//     {
//         if (timeRemaining > 0)
//         {
//             timeRemaining--;
//             UIManager.Instance.UpdateTimeText(timeRemaining);
//
//             if (timeRemaining <= 0)
//             {
//                 LoseLife();
//             }
//         }
//     }
//
//     public void AddScore(int points)
//     {
//         score += points;
//         UIManager.Instance.UpdateScoreText(score);
//     }
//     
//     public void LoseLife()
//     {
//         Debug.Log("LoseLife called!");
//         if (isTransitioning) return;
//         isTransitioning = true;
//         livesRemaining--;
//         UIManager.Instance.UpdateLivesText(livesRemaining);
//         if (livesRemaining > 0)
//         {
//             TransitionToStageScene();
//         }
//         else
//         {
//             EndGame();
//         }
//     }
//     
//     private void TransitionToStageScene()
//     {
//         Debug.Log("Transitioning to StageScene...");
//         SceneManager.LoadScene(stageSceneName);
//         StartCoroutine(WaitBeforeTransitionToStageScene());
//     }
//
//     private System.Collections.IEnumerator WaitBeforeTransitionToStageScene()
//     {
//         yield return new WaitForSeconds(2f);
//         RestartGame();
//     }
//     
//     
//     
//     private void RestartGame()
//     {
//         Debug.Log("Restarting Game...");
//         SceneManager.LoadScene(gameSceneName);
//         isTransitioning = false;
//         StartCoroutine(WaitForSceneLoad());
//     }
//
//     private System.Collections.IEnumerator WaitForSceneLoad()
//     {
//         yield return new WaitUntil(() => SceneManager.GetActiveScene().name == gameSceneName);
//
//         Debug.Log("GameScene loaded. Updating UI...");
//         UIManager.Instance.AssignUIComponents();
//         UIManager.Instance.UpdateTimeText(timeRemaining);
//         UIManager.Instance.UpdateLivesText(livesRemaining);
//         UIManager.Instance.UpdateScoreText(score);
//
//         StartTimer();
//     }
//
//     private void EndGame()
//     {
//         Debug.Log("Game Over!");
//         CancelInvoke(nameof(UpdateTimer));
//         SceneManager.LoadScene(gameOverSceneName);
//     }
// }