using System;
using UnityEngine;

[RequireComponent(typeof(AlfaChanger))]
public class Exploder : MonoBehaviour
{
    private const int OverlapSphereArraySize = 100;

    [SerializeField] private AlfaChanger _alfaChanger;
    [SerializeField] private float _exsplosionForce = 20f;
    [SerializeField] private float _baseRadius = 5f;
    [SerializeField] private float _upwardModifier = 0.5f;

    private Collider[] _targets;

    public event Action OnEndExploder;

    private void OnEnable()
    {
        _alfaChanger.EndChanger += OnExplode;
    }

    private void Awake()
    {
        _alfaChanger = GetComponent<AlfaChanger>();

        if (_targets == null)
        {
            _targets = new Collider[OverlapSphereArraySize];
        }
    }

    private void OnDisable()
    {
        _alfaChanger.EndChanger -= OnExplode;
    }

    private void OnExplode()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _baseRadius, _targets);

        foreach (Collider target in _targets)
        {
            if (target is not null && target.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_exsplosionForce, rigidbody.transform.position,
                    _baseRadius, _upwardModifier, ForceMode.Impulse);
                OnEndExploder?.Invoke();
            }
        }
    }
}