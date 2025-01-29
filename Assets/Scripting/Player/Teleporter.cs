using UnityEngine;

public class TilemapTeleporter : MonoBehaviour
{
    public Transform teleporterA;
    public Transform teleporterB;
    public KeyCode teleportKey = KeyCode.Z;
    public string playerTag = "Player";

    private void Update()
    {
        if (Input.GetKeyDown(teleportKey))
        {
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            if (player == null)
            {
                Debug.LogError("Player object not found!");
                return;
            }

            Vector3 playerPosition = player.transform.position;

            if (Vector3.Distance(playerPosition, teleporterA.position) < 0.5f)
            {
                TeleportPlayer(player, teleporterB.position);
            }
            else if (Vector3.Distance(playerPosition, teleporterB.position) < 0.5f)
            {
                TeleportPlayer(player, teleporterA.position);
            }
        }
    }

    private void TeleportPlayer(GameObject player, Vector3 targetPosition)
    {
        player.transform.position = targetPosition;
        Debug.Log("Player teleported to: " + targetPosition);
    }
}