using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private float explosionDelay = 3f; // זמן פיצוץ
    [SerializeField] private int maxBombs = 1; // כמות הפצצות המקסימלית שהשחקן יכול להניח
    private int currentBombs = 0; // כמות הפצצות הפעילות שהשחקן הניח כרגע

    private void Update()
    {
        // בדיקת קלט להנחת פצצה
        if (Input.GetKeyDown(KeyCode.X) && currentBombs < maxBombs)
        {
            PlaceBomb();
        }
    }

    private void PlaceBomb()
    {
        // קבלת פצצה מה-Pool
        Bomb bomb = BombPool.Instance.Get();

        // מיקום הפצצה: מיקום השחקן
        bomb.transform.position = transform.position;

        // הפעלת הפצצה
        bomb.Activate(explosionDelay, OnBombExploded);

        // עדכון כמות הפצצות הפעילות
        currentBombs++;
    }

    private void OnBombExploded(Bomb bomb)
    {
        // החזרת הפצצה ל-Pool
        BombPool.Instance.Return(bomb);

        // עדכון כמות הפצצות הפעילות
        currentBombs--;
    }

    public void IncreaseMaxBombs()
    {
        maxBombs++; // הגדלת כמות הפצצות המקסימלית
        Debug.Log($"Max bombs increased to {maxBombs}");
    }
}
