using UnityEngine;

public class Platform : CircleItem
{
    [SerializeField] private David _david;
    [SerializeField] private Texture _secretTexture;

    private ItemColor _secretColor;
    private ItemColor _temporaryColor;
    public David David => _david;
    public float AngleOnCircle { get; private set; }
    public bool IsSecret { get; private set; }

    private void OnEnable()
    {
        David.ColorExchanging += OnColorExchanging;
    }

    private void OnDisable()
    {
        David.ColorExchanging -= OnColorExchanging;
    }

    private void Start()
    {
        if (IsSecret)
        {
            SetTexture(_secretTexture);
            _secretColor = CurrentColor;
            SetItemColor(_temporaryColor);
        }
        else
        {
            _secretColor = new ItemColor();
        }
    }

    public void BecameSecret(ItemColor temporaryColor)
    {
        IsSecret = true;
        _temporaryColor = temporaryColor;
    }

    public void SetAngleOnCircle()
    {
        AngleOnCircle = GetAngleOnCircle();
        TurnToCenter(AngleOnCircle);
    }

    private void OnColorExchanging(Color color)
    {
        HitEffect.PlayEffect(color);

        if (color == _secretColor.MainColor)
        {
            SetTexture();
            SetItemColor(_secretColor);
        }
    }
}
