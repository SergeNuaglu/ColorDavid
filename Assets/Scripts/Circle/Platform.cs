using UnityEngine;
using UnityEngine.Events;

public class Platform : CircleItem
{
    [SerializeField] private David _david;
    [SerializeField] private Texture _secretTexture;
    [SerializeField] private ItemColor _defaultColor;

    private ItemColor _secretColor;
    public David David => _david;
    public float AngleOnCircle { get; private set; }
    public bool IsSecret { get; private set; }
    public bool IsSameColorWithDavid { get; private set; }

    public event UnityAction ColorMatched;
    public event UnityAction ColorExchanged;

    private void OnDisable()
    {
        David.ColorExchanging -= OnColorExchanging;
        Circle.AllColorsMatched -= OnAllColorsMatched;
    }

    private void Start()
    {
        David.ColorExchanging += OnColorExchanging;
        Circle.AllColorsMatched += OnAllColorsMatched;

        if (IsSecret)
        {
            SetTexture(_secretTexture);
            _secretColor = CurrentColor;
            SetItemColor(_defaultColor);
        }
        else
        {
            _secretColor = new ItemColor();
        }
    }

    public void BecameSecret()
    {
        IsSecret = true;
    }

    public void SetAngleOnCircle()
    {
        AngleOnCircle = GetAngleOnCircle();
        TurnToCenter(AngleOnCircle);
    }

    private void OnColorExchanging(Color color)
    {
        HitEffect.PlayEffect(color);

        if (IsSecret && color == _secretColor.MainColor)
        {
            SetTexture();
            SetItemColor(_secretColor);
            IsSecret = false;
            IsSameColorWithDavid = true;
        }
        else if (color == CurrentMainColor)
        {
            IsSameColorWithDavid = true;
            ColorMatched?.Invoke();
        }
        else
        {
            IsSameColorWithDavid = false;
        }

        ColorExchanged?.Invoke();
    }

    private void OnAllColorsMatched()
    {
        _david.CelebrateVictory();
    }
}
