using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // השחקן שאחריו המצלמה עוקבת
    public Transform bottomLeft; // נקודת הפינה השמאלית התחתונה
    public Transform topRight; // נקודת הפינה הימנית העליונה
    public float smoothSpeed = 0.125f; // מהירות תנועה חלקה של המצלמה

    private Vector3 targetPosition; // מיקום המטרה של המצלמה
    private Vector2 cameraDimensions; // גודל המצלמה

    void Start()
    {
        // חישוב גודל המצלמה
        cameraDimensions = new Vector2(
            Camera.main.aspect * Camera.main.orthographicSize * 2, // רוחב
            Camera.main.orthographicSize * 2                      // גובה
        );
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // לחשב את מיקום המטרה של המצלמה בהתבסס על מיקום השחקן
        targetPosition = player.position;

        // חישוב גבולות
        float minX = bottomLeft.position.x + cameraDimensions.x / 2;
        float maxX = topRight.position.x - cameraDimensions.x / 2;
        float minY = bottomLeft.position.y + cameraDimensions.y / 2;
        float maxY = topRight.position.y - cameraDimensions.y / 2;

        // הגבלת מיקום המצלמה לגבולות הלוח
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        // שמירת מיקום ה-Z של המצלמה
        targetPosition.z = transform.position.z;

        // תנועה חלקה לעבר מיקום המטרה
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}