using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour, ISpawnable<Cube>
{
    [SerializeField] private ColorChanger _colorChanger;

    private WaitForSeconds _waitForSeconds;
    private WaitForSeconds _waitForSecondsToDestroy;
    private Coroutine _currentCorutine;
    private Coroutine _failSafeCoroutine;

    private bool _isTouchPlatform = false;
    private bool _isReturned = false;

    private float _minLifeTimeValue = 2f;
    private float _liveTimeValue = 6f;
    private float _maxLifeTimeValue = 6f;

    public event Action<Cube> Destroyer;


    private void OnEnable()
    {
        _isReturned = false;
        _isTouchPlatform = false;

        if (_failSafeCoroutine != null)
        {
            StopCoroutine(FailSafeDestroy());
        }

        _failSafeCoroutine = StartCoroutine(FailSafeDestroy());
    }

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _waitForSecondsToDestroy = new WaitForSeconds(_liveTimeValue);
    }

    private IEnumerator FailSafeDestroy()
    {
        yield return _liveTimeValue;

        if (!_isTouchPlatform)
            ReturnToPool();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform component))
        {
            if (!_isTouchPlatform)
            {
                _isTouchPlatform = true;
                _colorChanger.SetColor();

                if (_currentCorutine != null)
                {
                    StopCoroutine(_currentCorutine);
                }

                _waitForSeconds = new WaitForSeconds(Random.Range(_minLifeTimeValue, _maxLifeTimeValue));

                _currentCorutine = StartCoroutine(CubeLifecycleRoutine());
            }
        }
    }

    private void OnDisable()
    {
        if (!_isReturned)
        {
            ReturnToPool();
        }

        if (_currentCorutine != null)
            StopCoroutine(_currentCorutine);
    }

    public void Reset()
    {
        _colorChanger.ResetColor();
    }

    private IEnumerator CubeLifecycleRoutine()
    {
        yield return _waitForSeconds;
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (_isReturned) return;

        _isReturned = true;
        Destroyer?.Invoke(this);
    }
}