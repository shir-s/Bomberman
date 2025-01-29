using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorInteraction : MonoBehaviour
{
    public Tilemap doorTilemap;
    public GameManager gameManager; // הפניה ל-GameManager

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3Int playerCellPosition = doorTilemap.WorldToCell(transform.position);
            if (doorTilemap.HasTile(playerCellPosition) && gameManager.AreAllEnemiesDefeated())
            {
                Debug.Log("All enemies defeated! Transitioning to Win Screen...");
                gameManager.WinGame();
            }
            else
            {
                Debug.Log("Cannot use the door. Either enemies are still alive or no door is nearby.");
            }
        }
    }
}