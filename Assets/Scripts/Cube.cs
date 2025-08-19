using System;
using System.Collections;
using UnityEngine;


public class Cube : MonoBehaviour
{
    //private Rigidbody _rigidbody;
    private Coroutine _currentCorutine;
    private Renderer _currentRenderer;
    private float _lifeTime =3f;

    //private void OnCollisionEnter()
    //{
       
    //    FindObjectOfType<Spawner>().ReturnPoolObject(this);

    //           // ���������� ��� � ��� ��� ������������ � ����� ������������
    //}

    private void Start()
    {
        if (_currentCorutine==null)
        {
            _currentCorutine =  StartCoroutine(CubeLifecycleRoutine());
        }
    }

    private IEnumerator CubeLifecycleRoutine()
    {
        float time = 0f;

        while (time<_lifeTime)
        {
            _currentRenderer.material.color = UnityEngine.Random.ColorHSV();
            time += Time.deltaTime;

            yield return null; // ���� ��������� ����
        }
            FindObjectOfType<Spawner>().ReturnPoolObject(this);
    }
}
