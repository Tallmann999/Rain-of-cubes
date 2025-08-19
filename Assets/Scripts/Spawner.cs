using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Vector3 _centralSpawnPoint;
    private GenericObjectPool<Cube> _cubePool;
    

    private void Start()
    {
        _cubePool = new GenericObjectPool<Cube>(_prefab,10);

    }

    public void CreateRandomCubeRain()
    {

        for (int i = 0; i < _cubePool.CurrentCount; i++)
        {
            Cube newCube = _cubePool.GetObject();
            newCube.transform.position = new Vector3(Random.Range(0,2),6, Random.Range(2, 7));
        }
        // Здесь пишем логику создание обьектов  в разных точках, колличество обьектов.

    }

    public void ReturnPoolObject(Cube cube)
    {
        _cubePool.ReturnObject(cube);
    }

}
