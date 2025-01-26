using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("UI Text Components")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    /// <summary>
    /// Update the time text on the panel.
    /// </summary>
    public void UpdateTimeText(int time)
    {
        if (timeText != null)
        {
            timeText.text = "  TIME " + time;
        }
    }

    /// <summary>
    /// Update the score text on the panel.
    /// </summary>
    public void UpdateScoreText(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString("D2"); // שומר על מבנה של 2 ספרות
        }
    }

    /// <summary>
    /// Update the lives text on the panel.
    /// </summary>
    public void UpdateLivesText(int lives)
    {
        if (livesText != null)
        {
            livesText.text = "LEFT " + lives +"  ";
        }
    }
}