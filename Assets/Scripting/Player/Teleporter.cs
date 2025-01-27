using UnityEngine;

public class TilemapTeleporter : MonoBehaviour
{
    public Transform teleporterA; // אובייקט הטלפורטר הראשון
    public Transform teleporterB; // אובייקט הטלפורטר השני
    public KeyCode teleportKey = KeyCode.Z; // כפתור להפעלת הטלפורטר
    public string playerTag = "Player"; // ה-Tag שמסמן את השחקן

    private void Update()
    {
        if (Input.GetKeyDown(teleportKey))
        {
            GameObject player = GameObject.FindGameObjectWithTag(playerTag); // מציאת השחקן לפי Tag
            if (player == null)
            {
                Debug.LogError("Player object not found!");
                return;
            }

            Vector3 playerPosition = player.transform.position;

            // בדיקה האם השחקן נמצא בטלפורטר A
            if (Vector3.Distance(playerPosition, teleporterA.position) < 0.5f)
            {
                TeleportPlayer(player, teleporterB.position);
            }
            // בדיקה האם השחקן נמצא בטלפורטר B
            else if (Vector3.Distance(playerPosition, teleporterB.position) < 0.5f)
            {
                TeleportPlayer(player, teleporterA.position);
            }
        }
    }

    private void TeleportPlayer(GameObject player, Vector3 targetPosition)
    {
        player.transform.position = targetPosition; // עדכון מיקום השחקן
        Debug.Log("Player teleported to: " + targetPosition);
    }
}