using UnityEngine;

public class ColoredItem : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    private ColorOfItem _currentColor;
    private readonly int _shadedColorID = Shader.PropertyToID("_ColorDim");
    public ColorOfItem CurrentColor => _currentColor;
    public Color CurrentMainColor => _currentColor.MainColor;
    private void Awake()
    {
        _renderer.material.shader = Shader.Find(_renderer.material.shader.name);
    }

    public void SetColor(ColorOfItem newColor)
    {
        _currentColor = newColor;
        _renderer.material.color = newColor.MainColor;
        _renderer.material.SetColor(_shadedColorID, newColor.ShadedColor);
    }
}
