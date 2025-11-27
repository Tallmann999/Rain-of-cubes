using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : SpawnerBase<Cube>
{
    [SerializeField] private float _minSpawnLength = 0f;
    [SerializeField] private float _maxSpawnLength = 3f;
    [SerializeField] private float _spawnHeight = 5f;

    private Coroutine _currentCoroutine;
    private WaitForSeconds _waitForSecond;
    private float _minLiveTimeValue = 0f;
    private float _maxLiveTimeValue = 1f;

    public event Action<Cube> Completed;

    private void Start()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(CubeGenerator());
        }

        _currentCoroutine = StartCoroutine(CubeGenerator());
    }

    protected override void OnReturnPoolObject(Cube cube)
    {
        cube.Destroyer -= OnReturnPoolObject;
        OnDisappeared(cube);
        PoolObject.ReturnObject(cube);
        UpdateCounter();
    }

    protected override void GreateNewPoolObject(out Cube newCube)
    {
        newCube = PoolObject.GetObject();
        newCube.Destroyer -= OnReturnPoolObject;
        newCube.Destroyer += OnReturnPoolObject;

        CountSpawn();
        newCube.transform.position = GenerateRandomPosition();
    }

    private IEnumerator CubeGenerator()
    {
        _waitForSecond = new WaitForSeconds((Random.Range(_minLiveTimeValue, _maxLiveTimeValue)));

        for (int i = 0; i < PoolObjectCount; i++)
        {
            Cube newCube;
            GreateNewPoolObject(out newCube);
            yield return _waitForSecond;
        }
    }

    private void OnDisappeared(Cube cube)
    {
        Completed?.Invoke(cube);
    }
  
    private Vector3 GenerateRandomPosition()
    {
        float positionX = Random.Range(_minSpawnLength, _maxSpawnLength);
        float positionZ = Random.Range(_minSpawnLength, _maxSpawnLength);

        return new Vector3(positionX, _spawnHeight, positionZ);
    }
}
