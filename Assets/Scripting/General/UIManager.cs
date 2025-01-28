using TMPro;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("UI Text Components")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        AssignUIComponents();
    }

    public void AssignUIComponents()
    {
        if (timeText == null)
        {
            timeText = GameObject.Find("TimeText")?.GetComponent<TextMeshProUGUI>();
            if (timeText == null) Debug.LogError("TimeText could not be found in the scene!");
        }

        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
            if (scoreText == null) Debug.LogError("ScoreText could not be found in the scene!");
        }

        if (livesText == null)
        {
            livesText = GameObject.Find("LivesText")?.GetComponent<TextMeshProUGUI>();
            if (livesText == null) Debug.LogError("LivesText could not be found in the scene!");
        }
    }

    public void UpdateTimeText(int time)
    {
        if (timeText != null)
        {
            timeText.text = "  TIME " + time;
        }
    }

    public void UpdateScoreText(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString("D2");
        }
    }

    public void UpdateLivesText(int lives)
    {
        if (livesText != null)
        {
            livesText.text = "LEFT " + lives + "  ";
        }
    }
}