using UnityEngine;

public class Platform : CircleItem
{
    [SerializeField] private David _david;

    public David David => _david;
    public float AngleOnCircle { get; private set; }

    private void OnEnable()
    {
        David.ColorExchanging += OnColorExchanging;
    }

    private void OnDisable()
    {
        David.ColorExchanging -= OnColorExchanging;
    }

    public void SetAngleOnCircle()
    {
        AngleOnCircle = GetAngleOnCircle();
    }

    private void OnColorExchanging(Color color)
    {
        HitEffect.PlayEffect(color);
    }
}
