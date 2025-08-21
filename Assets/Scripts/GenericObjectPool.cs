using UnityEditor.Search;
using UnityEngine;
using System.Collections.Generic;

public class GenericObjectPool<T> where T : Cube
{
    private T _prefab;
    private Queue<T> _pool = new Queue<T>();
    private Transform _transform;

    public GenericObjectPool(T prefab, int initializeSize)
    {
        _prefab = prefab;

        for (int i = 0; i < initializeSize; i++)
        {
            T newObject = GameObject.Instantiate(prefab);
            newObject.gameObject.SetActive(false);
            _pool.Enqueue(newObject);
        }

        Debug.Log("Здесь добавляем префабы в очередь");
    }

    public T GetObject()
    {
        if (_pool.Count == 0)
        {
            T obj = GameObject.Instantiate(_prefab);
            return obj;
        }

        T pooledObj = _pool.Dequeue();
        pooledObj.gameObject.SetActive(true);
        Debug.Log("Активируем префаб");
        return pooledObj;
    }

    public void ReturnObject(T poolObject)
    {
        poolObject.gameObject.SetActive(false);
        Debug.Log("Возвращаем  префаб назад");
        _pool.Enqueue(poolObject);
    }
}
