using System;
using UnityEngine;

public class BricksExplosion : MonoBehaviour
{
    public float explosionTime = 1f;

    private void Start()
    {
        Destroy(gameObject, explosionTime);
    }
}
