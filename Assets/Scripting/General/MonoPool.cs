using System;
using UnityEngine;
using System.Collections.Generic;

public class MonoPool<T> : MonoSingleton<MonoPool<T>> where T : MonoBehaviour,IPoolable
{
    [SerializeField] private int initialSize;
    [SerializeField] private T prefab;
    [SerializeField] private Transform parent;
    private Stack<T> _available;
    private List<T> _active; 
    public void Awake()
    {
        _available = new Stack<T>();
        _active = new List<T>();
        for (int i = 0; i < initialSize; i++)
        {
            AddItemsToPool();
        }
    }
    
    public T Get()
    {
        if (_available.Count == 0)
        {
            AddItemsToPool();
        }
        var obj=_available.Pop();
        obj.gameObject.SetActive(true);
        obj.Reset();
        _active.Add(obj);
        return obj;
    }

    private void AddItemsToPool()
    {
        var obj = Instantiate(prefab,parent,true);
        obj.gameObject.SetActive(false);
        _available.Push(obj);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _available.Push(obj);
        _active.Remove(obj); 
    }
    
    public List<T> GetActiveObjects()
    {
        return _active;
    }
}