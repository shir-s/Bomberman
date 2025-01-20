using UnityEngine;

public class Bomb : MonoBehaviour, IPoolable
{
    private System.Action<Bomb> onExploded; // פעולה שתופעל כאשר הפצצה מתפוצצת

    public void Reset()
    {
        // איפוס מצב הפצצה
        CancelInvoke(); // ביטול כל פעולות Invoke
    }

    public void Activate(float delay, System.Action<Bomb> onExplodedCallback)
    {
        onExploded = onExplodedCallback; // שמירת הפעולה שתופעל בזמן הפיצוץ
        Invoke(nameof(Explode), delay); // הגדרת זמן לפיצוץ
    }

    private void Explode()
    {
        Debug.Log("Boom!"); // לוג של הפיצוץ
        onExploded?.Invoke(this); // קריאה חזרה ל-BombController
    }
}
