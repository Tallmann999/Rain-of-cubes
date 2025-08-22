using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _cubeSpawnCount = 40;

    private GenericObjectPool<Cube> _cubePool;
    private Coroutine _currentCoroutine;
    private int _poolObjectCount = 10;
    private WaitForSeconds _waitForSecond;
    private float _minLiveTimeValue = 0f;
    private float _maxLiveTimeValue = 1f;


    private void Awake()
    {
        _cubePool = new GenericObjectPool<Cube>(_prefab, _poolObjectCount);
        _waitForSecond = new WaitForSeconds((Random.Range(_minLiveTimeValue, _maxLiveTimeValue)));
    }

    private void Start()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(CreateRandomCubeSpawn());
        }

        _currentCoroutine = StartCoroutine(CreateRandomCubeSpawn());
    }

    private IEnumerator CreateRandomCubeSpawn()
    {
        float minRandomValue = 0f;
        float maxRandomValue = 3f;
        float randomValue = Random.Range(minRandomValue, maxRandomValue);

        for (int i = 0; i < _cubeSpawnCount; i++)
        {
            Cube newCube = _cubePool.GetObject();
            newCube.ResetColor();
            newCube.transform.position = new Vector3(Random.Range(minRandomValue, maxRandomValue),
                transform.position.y, 0f);
            newCube.CubeReturn += ReturnPoolObject;
            yield return _waitForSecond;
        }
    }

    private void ReturnPoolObject(Cube cube)
    {
        cube.CubeReturn -= ReturnPoolObject;
        _cubePool.ReturnObject(cube);
    }
}
