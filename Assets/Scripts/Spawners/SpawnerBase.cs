using System;
using UnityEngine;

public abstract class SpawnerBase<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected GenericObjectPool<T> PoolObject;
    [SerializeField] protected int PoolCapacity = 40;
    [SerializeField] protected int PoolObjectCount = 10;

    private EntityCounter _entityCounter;
    private int _spawnedAllTime = 0;

    public event Action<EntityCounter> Updated;

    private void Awake()
    {
        PoolObject = new GenericObjectPool<T>(Prefab, PoolObjectCount);
        _entityCounter = new EntityCounter();
    }

    protected void CountSpawn()
    {
        _spawnedAllTime++;  
        UpdateCounter();   
    }

    protected void UpdateCounter()
    {
        _entityCounter.Update(spawned: _spawnedAllTime,
            created: PoolObject.CreatedObject,active: PoolObject.ActiveObject);
        Updated?.Invoke(_entityCounter);
    }

    protected abstract void GreateNewPoolObject( out T prefab);
    protected abstract void OnReturnPoolObject(T prefab);
}
