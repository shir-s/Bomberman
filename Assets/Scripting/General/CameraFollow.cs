using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // השחקן שאחריו המצלמה עוקבת
    public Vector2 minBounds; // גבול מינימלי של המצלמה
    public Vector2 maxBounds; // גבול מקסימלי של המצלמה
    public float smoothSpeed = 0.125f; // מהירות תנועה חלקה של המצלמה
    public Vector2 cameraSize; // גודל המצלמה בשטח העולם

    private Vector3 targetPosition; // מיקום המטרה של המצלמה

    private void LateUpdate()
    {
        if (player == null) return;

        // לחשב את מיקום המטרה של המצלמה בהתבסס על מיקום השחקן
        targetPosition = player.position;

        // הגבלת מיקום המצלמה לגבולות הלוח
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x + cameraSize.x / 2, maxBounds.x - cameraSize.x / 2);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y + cameraSize.y / 2, maxBounds.y - cameraSize.y / 2);

        // שמירת מיקום ה-Z של המצלמה
        targetPosition.z = transform.position.z;

        // תנועה חלקה לעבר מיקום המטרה
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
    
    // public Transform player; // השחקן שאחריו המצלמה עוקבת
    // public Vector2 minBounds; // גבול מינימלי של המצלמה
    // public Vector2 maxBounds; // גבול מקסימלי של המצלמה
    // public float smoothSpeed = 0.125f; // מהירות תנועה חלקה של המצלמה
    // public Vector2 cameraSize; // גודל המצלמה בשטח העולם
    //
    // private Vector3 targetPosition; // מיקום המטרה של המצלמה
    //
    // private void LateUpdate()
    // {
    //     if (player == null) return;
    //
    //     // לחשב את מיקום המטרה של המצלמה בהתבסס על מיקום השחקן
    //     targetPosition = transform.position;
    //
    //     // בדיקה אם השחקן עבר חצי מהמסך בכיוון X
    //     if (player.position.x > transform.position.x + cameraSize.x / 2)
    //     {
    //         targetPosition.x += cameraSize.x; // להזיז את המצלמה ימינה
    //     }
    //     else if (player.position.x < transform.position.x - cameraSize.x / 2)
    //     {
    //         targetPosition.x -= cameraSize.x; // להזיז את המצלמה שמאלה
    //     }
    //
    //     // בדיקה אם השחקן עבר חצי מהמסך בכיוון Y
    //     if (player.position.y > transform.position.y + cameraSize.y / 2)
    //     {
    //         targetPosition.y += cameraSize.y; // להזיז את המצלמה למעלה
    //     }
    //     else if (player.position.y < transform.position.y - cameraSize.y / 2)
    //     {
    //         targetPosition.y -= cameraSize.y; // להזיז את המצלמה למטה
    //     }
    //
    //     // הגבלת מיקום המצלמה לגבולות הלוח
    //     targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x + cameraSize.x / 2, maxBounds.x - cameraSize.x / 2);
    //     targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y + cameraSize.y / 2, maxBounds.y - cameraSize.y / 2);
    //
    //     // תנועה חלקה לעבר מיקום המטרה
    //     transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    // }
    //
}
