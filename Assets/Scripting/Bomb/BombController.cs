using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public KeyCode inputKey = KeyCode.Space;
    [SerializeField] private float explosionDelay = 3f; // זמן פיצוץ
    [SerializeField] private int bombAmount = 1;
    [SerializeField] private GameObject bombPrefab;
    private int bombRemaining;
    
    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("BricksExplosion")] [SerializeField]
    public Tilemap bricksOnTilemap;
    private BricksExplosion bricksPrefab;
    
    private void OnEnable()
    {
        bombRemaining = bombAmount;
    }

    private void Update() 
    {
        if (bombRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(DropBomb());
        }
    }
    
    private IEnumerator DropBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        
        Bomb bomb = MonoPool<Bomb>.Instance.Get();
        if (bomb == null)
        {
            Debug.LogError("No bomb available in the pool!");
            yield break;
        }
        bomb.transform.position = position;
        bomb.gameObject.SetActive(true); 
        bombRemaining--;

        yield return new WaitForSeconds(explosionDelay);
        
        //Adding the Explosion
        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        Destroy(explosion.gameObject, explosionDuration);
        
        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);
        
        bomb.ReturnToPool();
        bombRemaining++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
            // StartCoroutine(DisableTriggerAfterDelay(other));
        }
    }
    
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }
        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            RemoveBrick(position);
            return;
        }
        
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        Destroy(explosion.gameObject, explosionDuration);
        
        Explode(position, direction, length - 1);
    }
    
    private void RemoveBrick(Vector2 position)
    {
        Vector3Int cell = bricksOnTilemap.WorldToCell(position);
        TileBase tile = bricksOnTilemap.GetTile(cell);
        if (tile != null)
        {
            Instantiate(bricksPrefab, position, Quaternion.identity);
            bricksOnTilemap.SetTile(cell, null);
        }
    }
}
