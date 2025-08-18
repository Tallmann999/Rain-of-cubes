using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ObjectPool<GameObject> _prefabs;
}
