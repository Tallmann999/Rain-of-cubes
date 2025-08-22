using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Renderer _currentRenderel;
    private Color _defaltColor = Color.white;

    private void Awake()
    {
        _currentRenderel = GetComponent<Renderer>();
    }

    public void SetColor()
    {
        _currentRenderel.material.color = Random.ColorHSV();
    }

    public void ResetColor()
    {
        _currentRenderel.material.color = _defaltColor;
    }
}
