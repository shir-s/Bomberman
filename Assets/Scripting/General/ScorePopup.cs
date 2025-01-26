using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    public float duration = 1.5f; // משך הזמן שהספרייט יישאר
    public Vector3 offset = new Vector3(0, 1, 0); // מרחק מעל מיקום הפיצוץ
    public float fadeSpeed = 1f; // מהירות הדהייה

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Destroy(gameObject, duration); // השמדת האובייקט לאחר זמן מוגדר
    }

    private void Update()
    {
        // הזזת האובייקט כלפי מעלה
        transform.position += offset * Time.deltaTime;

        // דהייה
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = color;
        }
    }
}
