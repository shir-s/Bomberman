using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        MoreBombs,
        BiggerRadius,
        FasterMovement,
    }
    
    public ItemType itemType;

    private void OnItemPickup(GameObject player)
    {
        switch (itemType)
        {
            case ItemType.MoreBombs:
                player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.BiggerRadius:
                player.GetComponent<BombController>().explosionRadius++;
                break;
            case ItemType.FasterMovement:
                player.GetComponent<PlayerController>().moveSpeed++;
                break;
        }
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemPickup(other.gameObject);
        }
    }
}
