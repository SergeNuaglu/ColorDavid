using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Renderer))]

public class David : MonoBehaviour, IColoredItem
{
    [SerializeField] Hammer _hammer;

    private Renderer _renderer;
    private Animator _animator;
    private const string Hit = "Hit";

    public ItemColor CurrentColor {get;private set;}

    public event UnityAction<Color> ColorExchanging;

    private void OnEnable()
    {
        _hammer.BowlHit += OnBowlHit;
    }

    private void OnDisable()
    {
        _hammer.BowlHit -= OnBowlHit;
    }

    private void Awake()
    {
        _renderer= GetComponent<Renderer>();
        _animator = GetComponent<Animator>();
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

    private void OnBowlHit(IColoredItem colorItem)
    {
        ColorExchanging?.Invoke(colorItem.CurrentColor.MainColor);
        ExchangeColors(colorItem);
    }

    private void ExchangeColors(IColoredItem colorItem)
    {
        ItemColor tempColor = CurrentColor;
        SetItemColor(colorItem.CurrentColor);
        colorItem.SetItemColor(tempColor);
    }

    public void SetItemColor(ItemColor newColor)
    {
        CurrentColor = newColor;
        _renderer.material.color = CurrentColor.MainColor;
    }
}
