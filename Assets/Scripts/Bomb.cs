using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Exploder))]
public class Bomb : MonoBehaviour, ISpawnable<Bomb>
{
    [SerializeField] private Exploder _exploder;

    private Coroutine _currentCorutine;
    private WaitForSeconds _waitForSeconds;
    private bool _isReturned = false;
    private float _lifeTimeValue = 6f;

    public event Action<Bomb> Destroyer;

    private void OnEnable()
    {
        _isReturned = false;
        _exploder.OnEndExploder += OnDestroyer;

        if (_currentCorutine != null)
        {
            StopCoroutine(AutoReturn());
        }

        _currentCorutine = StartCoroutine(AutoReturn());
    }

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _waitForSeconds = new WaitForSeconds(_lifeTimeValue);
    }

    private void OnDisable()
    {
        if (!_isReturned)
            ReturnToPool();

        _exploder.OnEndExploder -= OnDestroyer;
    }

    private IEnumerator AutoReturn()
    {
        yield return _waitForSeconds;

        if (gameObject.activeSelf)
            ReturnToPool();
    }

    private void OnDestroyer()
    {
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (_isReturned) return;

        _isReturned = true;
        Destroyer?.Invoke(this);
    }

    public void SetPosition(Vector3 position) => transform.position = position;
}