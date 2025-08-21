using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _cubeCount = 100;
    [SerializeField] private Color _defaultCubeColor = Color.white;
    private GenericObjectPool<Cube> _cubePool;
    private Vector3 _centralSpawnPoint;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _centralSpawnPoint = transform.position;
    }

    private void Start()
    {
        _cubePool = new GenericObjectPool<Cube>(_prefab, 10);

        if (_currentCoroutine!=null)
        {
            StopCoroutine(CreateRandomCubeRain());
        }

        _currentCoroutine = StartCoroutine(CreateRandomCubeRain());
    }

    public IEnumerator CreateRandomCubeRain()
    {
        for (int i = 0; i < _cubeCount; i++)
        {
            Cube newCube = _cubePool.GetObject();
            newCube.ResetColor();// 
            yield return new WaitForSeconds((Random.Range(0, 2)));
            newCube.transform.position = new Vector3((Random.Range(0, 2)), 6, Random.Range(0, 2));
            newCube.CubeReturn += ReturnPoolObject;
            Debug.Log("Здесь ждём появление нового куба");
            yield return null;
        }
    }

    public void ReturnPoolObject(Cube cube)
    {
        cube.CubeReturn -= ReturnPoolObject;
        _cubePool.ReturnObject(cube);
        
        Debug.Log("Здесь возвращаем куб");
    }
}
