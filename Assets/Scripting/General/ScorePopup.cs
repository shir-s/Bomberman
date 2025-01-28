using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    public float duration = 1.5f;
    public float delay = 0.1f;
    public Vector3 offset = new Vector3(0, 1, 0);
    public float fadeSpeed = 0.5f;

    private SpriteRenderer spriteRenderer;

    // private void Start()
    // {
    //     spriteRenderer = GetComponent<SpriteRenderer>();
    //     Destroy(gameObject, duration);
    // }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        StartCoroutine(ShowPopupWithDelay());
    }
    
    private System.Collections.IEnumerator ShowPopupWithDelay()
    {
        // השהייה של 2 שניות
        yield return new WaitForSeconds(delay);

        // מציג את הפופאפ
        spriteRenderer.enabled = true;

        // משמיד את האובייקט לאחר `duration` שניות נוספות
        Destroy(gameObject, duration);
    }
    
    private void Update()
    {
        // הזזת האובייקט כלפי מעלה
        //transform.position += offset * Time.deltaTime;

        // דהייה
        if (spriteRenderer != null && spriteRenderer.enabled)
        {
            Color color = spriteRenderer.color;
            color.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = color;
        }
    }
}
