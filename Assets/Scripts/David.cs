using UnityEngine;

[RequireComponent(typeof(Animator))]
public class David : ColoredItem
{
    [SerializeField] Hammer _hammer;

    private float _positionAngle;
    private Platform _platform;
    private Animator _animator;
    private const string Hit = "Hit";

    public float PositionAngle => _positionAngle;

    private void OnEnable()
    {
        _hammer.BowlHit += OnBowlHit;
    }

    private void OnDisable()
    {
        _hammer.BowlHit -= OnBowlHit;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Init(Platform platform, float positionAngle)
    {
        _platform = platform;
        _positionAngle = positionAngle;
    }

    public bool IsHitAnimationPlaying()
    {
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return animatorStateInfo.IsName("Base Layer.Hit");
    }

    public void HitBowl()
    {
        _animator.SetTrigger(Hit);
    }

    private void OnBowlHit(ColoredItem colorItem)
    {
        _platform.CreatePool(colorItem.CurrentMainColor);
        ExchangeColors(colorItem);
    }

    private void ExchangeColors(ColoredItem colorItem)
    {
        ColorOfItem tempColor = CurrentColor;
        SetColor(colorItem.CurrentColor);
        colorItem.SetColor(tempColor);
    }
}
