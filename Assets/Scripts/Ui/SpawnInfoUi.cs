using TMPro;
using UnityEngine;

public class SpawnInfoUi<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
{
    [SerializeField] private SpawnerBase<T> _spawnerBase;
    [SerializeField] private TextMeshProUGUI _allSpawnObjectCount;
    [SerializeField] private TextMeshProUGUI _createrSpawnObjectCount;
    [SerializeField] private TextMeshProUGUI _activeSpawnObjectCount;

    private void OnEnable()
    {
        _spawnerBase.Updated += UpdateInfo;
    }

    private void OnDisable()
    {
        _spawnerBase.Updated -= UpdateInfo;
    }

    private void UpdateInfo(EntityCounter counter)
    {
        _allSpawnObjectCount.text = counter.SpawnedObjects.ToString();
        _createrSpawnObjectCount.text = counter.CreatedObjects.ToString();
        _activeSpawnObjectCount.text = counter.ActiveObjects.ToString();
    }
}