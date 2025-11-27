using UnityEngine;
using System.Collections.Generic;

public class GenericObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Queue<T> _pool = new Queue<T>();

    public int ActiveObject { get; private set; }

    public int CreatedObject { get; private set; }

    public GenericObjectPool(T prefab, int initializeSize)
    {
        _prefab = prefab;

        for (int i = 0; i < initializeSize; i++)
        {
            T newObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newObject.gameObject.SetActive(false);
            _pool.Enqueue(newObject);
            CreatedObject++;
        }
    }

    public T GetObject()
    {
        T obj;

        if (_pool.Count == 0)
        {
            obj = GameObject.Instantiate(_prefab, Vector3.zero, Quaternion.identity);
            CreatedObject++;
            return obj;
        }
        else
        {
            obj = _pool.Dequeue();
        }

        ActiveObject++;
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnObject(T poolObject)
    {
        _pool.Enqueue(poolObject);
        ActiveObject--;
        poolObject.gameObject.SetActive(false);
    }
}