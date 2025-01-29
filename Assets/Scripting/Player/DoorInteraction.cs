using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorInteraction : MonoBehaviour
{
    public Tilemap doorTilemap; // Tilemap של הדלת
    public GameManager gameManager; // הפניה ל-GameManager
    public AudioClip doorSound; // קליפ הסאונד לדלת
    private AudioSource audioSource; // AudioSource להשמעת הסאונד
    private bool soundPlayed = false; // משתנה למניעת ניגון חוזר של הסאונד

    private void Awake()
    {
        // יצירת AudioSource אם לא קיים
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (doorSound != null)
        {
            audioSource.clip = doorSound;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // בדיקה אם השחקן נכנס לקוליידר של הדלת
        if (other.CompareTag("Player"))
        {
            // בדיקה אם כל האויבים הובסו
            if (doorTilemap != null && gameManager.AreAllEnemiesDefeated())
            {
                if (!soundPlayed)
                {
                    soundPlayed = true; // מניעת הפעלה חוזרת
                    audioSource.Play(); // נגן את הסאונד
                    Debug.Log("Player touched the door. Playing sound...");

                    // מעבר למסך הניצחון לאחר 2 שניות
                    Invoke(nameof(TransitionToWin), 2f);
                }
            }
            else
            {
                Debug.Log("Player touched the door, but enemies are still alive.");
            }
        }
    }

    private void TransitionToWin()
    {
        gameManager.WinGame(); // מעבר למסך הניצחון
    }
}


// using UnityEngine;
// using UnityEngine.Tilemaps;
//
// public class DoorInteraction : MonoBehaviour
// {
//     public Tilemap doorTilemap;
//     public GameManager gameManager; // הפניה ל-GameManager
//
//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.W))
//         {
//             Vector3Int playerCellPosition = doorTilemap.WorldToCell(transform.position);
//             if (doorTilemap.HasTile(playerCellPosition) && gameManager.AreAllEnemiesDefeated())
//             {
//                 Debug.Log("All enemies defeated! Transitioning to Win Screen...");
//                 gameManager.WinGame();
//             }
//             else
//             {
//                 Debug.Log("Cannot use the door. Either enemies are still alive or no door is nearby.");
//             }
//         }
//     }
// }