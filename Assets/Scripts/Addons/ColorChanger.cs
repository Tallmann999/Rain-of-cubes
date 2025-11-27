using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _currentRenderer;
    private Color _defaltColor = Color.white;

    private void Awake()
    {
        _currentRenderer = GetComponent<Renderer>();
    }

    public void SetColor()
    {
        _currentRenderer.material.color = Random.ColorHSV();
    }

    public void ResetColor()
    {
        _currentRenderer.material.color = _defaltColor;
    }
}