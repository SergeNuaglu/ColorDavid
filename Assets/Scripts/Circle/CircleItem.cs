using UnityEngine;
using UnityEngine.UIElements;

public class CircleItem : MonoBehaviour, IColoredItem
{
    [SerializeField] private HitEffect _hitEffect;
    [SerializeField] private Renderer _renderer;

    private readonly int _shadedColorID = Shader.PropertyToID("_ColorDim");
    private ItemColor _currentColor;
    private Texture _standartTexture;
    private Circle _circle;

    protected Circle Circle => _circle;

    public ItemColor CurrentColor => _currentColor;
    public Color CurrentMainColor => _currentColor.MainColor;
    public HitEffect HitEffect => _hitEffect;

    private void Awake()
    {
        _renderer.material.shader = Shader.Find(_renderer.material.shader.name);
        _standartTexture = _renderer.material.mainTexture;
    }

    public void Init(Circle circle)
    {
        _circle = circle;
    }


    public float GetAngleOnCircle()
    {
        float circleTotalAngle = 360f;
        Vector3 positionOnCircle;
        float sin;
        float cos;
        float result;

        positionOnCircle = GetPositionOnCircle();
        sin = positionOnCircle.x * Vector3.forward.z - Vector3.forward.x * positionOnCircle.z;
        cos = positionOnCircle.x * Vector3.forward.x + positionOnCircle.z * Vector3.forward.z;
        result = Mathf.Atan2(sin, cos) * ((circleTotalAngle / 2) / Mathf.PI);

        if (result < 0)
            return circleTotalAngle + result;

        return result;
    }

    public Vector3 GetPositionOnCircle()
    {
        float positionX;
        float positionZ;

        positionX = transform.position.x - _circle.transform.position.x;
        positionZ = transform.position.z - _circle.transform.position.z;
        return new Vector3(positionX, 0, positionZ);
    }

    public virtual void SetItemColor(ItemColor newColor)
    {
        _currentColor = newColor;
        _renderer.material.color = newColor.MainColor;
        _renderer.material.SetColor(_shadedColorID, newColor.ShadedColor);
    }

    protected void TurnToCenter(float angleOnCircle)
    {
        transform.rotation = Quaternion.Euler(0, angleOnCircle, 0);
    }

    protected void SetTexture(Texture texture = null)
    {
        if (texture == null)
            _renderer.material.mainTexture = _standartTexture;
        else
            _renderer.material.mainTexture = texture;
    }
}
