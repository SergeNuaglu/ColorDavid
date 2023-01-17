using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Renderer))]

public class David : MonoBehaviour, IColoredItem
{
    [SerializeField] Hammer _hammer;
    [SerializeField] ParticleSystem _freezeActivateEffect;

    private Renderer _renderer;
    private Animator _animator;
    private const string Hit = "Hit";
    private const string Freeze = "Freeze";
    private const string FreezeOff = "FreezeOff";

    public ItemColor CurrentColor { get; private set; }
    public bool IsFreezed { get; private set; }


    public event UnityAction<Color> ColorExchanging;

    private void OnEnable()
    {
        _hammer.BowlHit += OnBowlHit;
        _hammer.BowlIsFreezing += OnBowlFreezing;
    }

    private void OnDisable()
    {
        _hammer.BowlHit -= OnBowlHit;
        _hammer.BowlIsFreezing -= OnBowlFreezing;

    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _animator = GetComponent<Animator>();
    }

    public bool IsHitAnimationPlaying()
    {
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return animatorStateInfo.IsName("Base Layer.Hit");
    }

    public void SetItemColor(ItemColor newColor)
    {
        CurrentColor = newColor;
        _renderer.material.color = CurrentColor.MainColor;
    }

    public void HitBowl()
    { 
       _animator.SetTrigger(Hit);
    }

    public void Unfreeze()
    {
        _animator.SetTrigger(FreezeOff);
        IsFreezed = false;
    }

    private void OnBowlHit(IColoredItem colorItem)
    {
        ColorExchanging?.Invoke(colorItem.CurrentColor.MainColor);
        ExchangeColors(colorItem);
    }

    private void OnBowlFreezing()
    {
        _freezeActivateEffect.Play();
        _animator.SetTrigger(Freeze);
        IsFreezed = true;
    }

    private void ExchangeColors(IColoredItem colorItem)
    {
        ItemColor tempColor = CurrentColor;
        SetItemColor(colorItem.CurrentColor);
        colorItem.SetItemColor(tempColor);
    }

}
