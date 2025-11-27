using UnityEngine;

public class BombSpawner : SpawnerBase<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.Completed += OnSetSpawnPosition;
    }

    private void OnDisable()
    {
        _cubeSpawner.Completed -= OnSetSpawnPosition;
    }

    protected override void GreateNewPoolObject(out Bomb bomb)
    {
        bomb = PoolObject.GetObject();
        bomb.Destroyer += OnReturnPoolObject;
    }

    protected override void OnReturnPoolObject(Bomb bomb)
    {
        bomb.Destroyer -= OnReturnPoolObject;
        PoolObject.ReturnObject(bomb);
        UpdateCounter();
    }

    private void OnSetSpawnPosition(Cube cube)
    {
        Bomb bomb;
        GreateNewPoolObject(out bomb);
        bomb.SetPosition(cube.transform.position);
        CountSpawn();
    }
}
