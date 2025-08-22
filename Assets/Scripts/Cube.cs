using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    [SerializeField] private ColorChanger _colorChanger;

    public event Action<Cube> CubeReturn;
    private Coroutine _currentCorutine;
    private bool _isTouchPlatform = false;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnEnable()
    {
        _isTouchPlatform = false;
    }
       
    private void OnCollisionEnter(Collision collision)
    {
        float minValue = 2;
        float maxValue = 5;

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

                _currentCorutine = StartCoroutine(CubeLifecycleRoutine(Random.Range(minValue, maxValue)));
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

    private IEnumerator CubeLifecycleRoutine(float lifeTime)
    {
        float time = 0f;

        while (time < lifeTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        CubeReturn?.Invoke(this);
    }
}
