using System;
using System.Collections;
using UnityEngine;


public class Cube : MonoBehaviour
{
    public event Action<Cube> CubeReturn;
    private Coroutine _currentCorutine;
    private Renderer _currentRenderer;
    private float _lifeTime =3f;

    private void Awake()
    {
        _currentRenderer = GetComponent<Renderer>();
    }
   
    private void OnEnable()
    {
        if (_currentCorutine != null)
        {
            StopCoroutine(_currentCorutine);
        }

        _currentCorutine = StartCoroutine(CubeLifecycleRoutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.)
        //{

        //}
    }

    private void SetRandomColor()
    {
        _currentRenderer.material.color = UnityEngine.Random.ColorHSV();

    }

    private void OnDisable()
    {
        if (_currentCorutine != null)
        {
            StopCoroutine(_currentCorutine);
            _currentCorutine = null;
        }
    }

    private IEnumerator CubeLifecycleRoutine()
    {
        float time = 0f;

        while (time < _lifeTime)
        {
            time += Time.deltaTime;
            yield return null; 
        }

        CubeReturn?.Invoke(this);
        Debug.Log("Время жизни истекло, возвращаем куб: " + this);
    }
}
