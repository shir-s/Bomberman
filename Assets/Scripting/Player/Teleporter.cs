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
            Debug.Log("Teleport key pressed!");

            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            if (player == null)
            {
                Debug.LogError("Player object not found!");
                return;
            }

            Vector3 playerPosition = player.transform.position;
            Debug.Log($"Player position: {playerPosition}");

            if (Vector3.Distance(playerPosition, teleporterA.position) < 0.5f)
            {
                Debug.Log("Player near teleporter A. Teleporting to B.");
                TeleportPlayer(player, teleporterB.position);
            }
            else if (Vector3.Distance(playerPosition, teleporterB.position) < 0.5f)
            {
                Debug.Log("Player near teleporter B. Teleporting to A.");
                TeleportPlayer(player, teleporterA.position);
            }
            else
            {
                Debug.Log("Player is not near any teleporter.");
            }
        }
    }

    private void TeleportPlayer(GameObject player, Vector3 targetPosition)
    {
        player.transform.position = targetPosition;
        Debug.Log("Player teleported to: " + targetPosition);
    }
}