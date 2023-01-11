using UnityEngine;

public class CircleItem : MonoBehaviour, IColoredItem
{
    [SerializeField] private HitEffect _hitEffect;
    [SerializeField] private Renderer _renderer;

    private readonly int _shadedColorID = Shader.PropertyToID("_ColorDim");
    private ItemColor _currentColor;

    public ItemColor CurrentColor => _currentColor;
    public Color CurrentMainColor => _currentColor.MainColor;
    public HitEffect HitEffect => _hitEffect;    

    private void Awake()
    {
        _renderer.material.shader = Shader.Find(_renderer.material.shader.name);
        TurnToCenter(); 
    }

    public float GetAngleOnCircle()
    {
        float circleTotalAngle = 360f;
        float sin = transform.position.x * Vector3.forward.z - Vector3.forward.x * transform.position.z;
        float cos = transform.position.x * Vector3.forward.x + transform.position.z * Vector3.forward.z;
        float result = Mathf.Atan2(sin, cos) * ((circleTotalAngle / 2) / Mathf.PI);

        if (result < 0)
            return circleTotalAngle + result;

        return result;
    }
    public void SetItemColor(ItemColor newColor)
    {
        _currentColor = newColor;
        _renderer.material.color = newColor.MainColor;
        _renderer.material.SetColor(_shadedColorID, newColor.ShadedColor);
    }

    private void TurnToCenter()
    {
        transform.rotation = Quaternion.Euler(0, GetAngleOnCircle(), 0);
    }
}
