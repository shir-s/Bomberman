using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemsToDestruct : MonoBehaviour
{
    public float explosionTime = 1f;
    
    [Range(0f, 1f)]
    public float itemSpawnAmount = 0.2f;
    public GameObject[] itemsToSpawn;

    private void Start()
    {
        Destroy(gameObject, explosionTime);
    }

    private void OnDestroy()
    {
        if(itemsToSpawn.Length > 0 && Random.value < itemSpawnAmount)
        {
            int randomIndex = Random.Range(0, itemsToSpawn.Length);
            Instantiate(itemsToSpawn[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
