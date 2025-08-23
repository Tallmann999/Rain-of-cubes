using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    [SerializeField] private ColorChanger _colorChanger;      

    private WaitForSeconds _waitForSeconds;
    private Coroutine _currentCorutine;
    private bool _isTouchPlatform = false;
    private float _minLifeTimeValue = 2f;
    private float _maxLifeTimeValue = 5f;

    public event Action<Cube> CubeReturn;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _waitForSeconds = new WaitForSeconds(Random.Range(_minLifeTimeValue, _maxLifeTimeValue));
    }
    
    private void OnEnable()
    {
        _isTouchPlatform = false;
    }
       
    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.collider.TryGetComponent(out Platform component) == true)
        {
            if (!_isTouchPlatform)
            {
                _isTouchPlatform = true;
                _colorChanger.SetColor();

                if (_currentCorutine != null)
                {
                    StopCoroutine(_currentCorutine);
                }

                _currentCorutine = StartCoroutine(CubeLifecycleRoutine());
            }
        }
    }

    private void OnDisable()
    {
        if (_currentCorutine != null)
        {
            StopCoroutine(_currentCorutine);
            _currentCorutine = null;
        }

        _isTouchPlatform = false;
    }

    public void ResetColor()
    {
        _colorChanger.ResetColor();
    }

    private IEnumerator CubeLifecycleRoutine()
    {
        yield return _waitForSeconds;
        CubeReturn?.Invoke(this);
    }
}
