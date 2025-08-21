using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class Cube : MonoBehaviour
{
    public event Action<Cube> CubeReturn;
    private Coroutine _currentCorutine;
    private Renderer _currentRenderer;
    private bool _isTouchPlatform = false;
    private Color _defaultColor = Color.white;

    private void Awake()
    {
        _currentRenderer = GetComponent<Renderer>();
        ResetColor();

    }

    private void OnEnable()
    {
        _isTouchPlatform = false;
       ResetColor();
    }

    private void OnCollisionEnter(Collision collision)
    {     
        if (collision.collider.TryGetComponent(out PlatformCollision component) == true)
        {
            if (!_isTouchPlatform)
            {
                _isTouchPlatform = true;
                SetColor();

                if (_currentCorutine != null)
                {
                    StopCoroutine(_currentCorutine);
                }

                _currentCorutine = StartCoroutine(CubeLifecycleRoutine(Random.Range(2, 5)));
            }
        }
    }

    private void SetColor()
    {
        _currentRenderer.material.color = Color.red;
    }

    public void ResetColor()
    {
        _currentRenderer.material.color = _defaultColor;
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

    private IEnumerator CubeLifecycleRoutine(float lifeTime)
    {
        float time = 0f;

        while (time < lifeTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        CubeReturn?.Invoke(this);
        Debug.Log("Время жизни истекло, возвращаем куб: " + this);
    }
}
