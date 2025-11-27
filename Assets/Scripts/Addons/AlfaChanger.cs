using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AlfaChanger : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    private Coroutine _currentCoroutine;
    private Color _color;
    private float _randomValue;

    public event Action EndChanger;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;
    }

    private void Start()
    {
        _randomValue = GetRandomValue();
        ActivatorCoroutine();
    }

    public void ActivatorCoroutine()
    {
        if (_currentCoroutine == null)
        {
            StopCoroutine(FadeOutCoroutine());
        }

        _currentCoroutine = StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;
        float maxValue = 1f;
        float minValue = 0f;

        while (elapsedTime <= _randomValue)
        {
            float normalizedTime = elapsedTime / _randomValue;
            elapsedTime += Time.deltaTime;
            _color.a = Mathf.Lerp(maxValue, minValue, normalizedTime);
            _renderer.material.color = _color;
            yield return null;
        }

        EndChanger?.Invoke();
    }

    private float GetRandomValue()
    {
        float minValue = 2f;
        float maxValue = 5f;
        float randomValue = UnityEngine.Random.Range(minValue, maxValue);
        return randomValue;
    }
}