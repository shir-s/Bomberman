using System;
using UnityEngine;
using System.Collections;
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
    public float explosionDuration = 1f;
    public int explosionRadius = 1;
    
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
        Debug.Log("setactive explosion pos"); 
        Destroy(explosion.gameObject, explosionDuration);
        
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
    
    private IEnumerator DisableTriggerAfterDelay(Collider2D collider)
    {
        yield return new WaitForSeconds(2f);
        collider.isTrigger = false;
    }
}
