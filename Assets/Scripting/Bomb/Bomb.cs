using System;
using UnityEngine;

public class Bomb : MonoBehaviour, IPoolable
{
    private Collider2D _collider;
    private System.Action<Bomb> onExploded;
    private Animation _animation;

    private void OnEnable()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void Reset()
    {
        _collider.isTrigger = true;
        gameObject.SetActive(false);
        CancelInvoke();
    }

    public void Activate(float delay, System.Action<Bomb> onExplodedCallback)
    {
        gameObject.SetActive(true);
        _animation.Play();
        onExploded = onExplodedCallback;
        Invoke(nameof(Explode), delay);
    }

    private void Explode()
    {
        Debug.Log("Boom!");
        onExploded?.Invoke(this);
    }
    
    public void ReturnToPool()
    {
        MonoPool<Bomb>.Instance.Return(this);
    }
}
