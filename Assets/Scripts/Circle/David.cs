using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Renderer))]

public class David : MonoBehaviour, IColoredItem, IFreezable
{
    [SerializeField] private ParticleSystem _freezeActivateEffect;
    [SerializeField] private Transform _hammerParent;
    [SerializeField] private Transform _glassesParent;

    private Hammer _hammer;
    private Glasses _glasses;
    private Renderer _renderer;
    private Animator _animator;
    private Game _game;

    public Hammer Hammer => _hammer;
    public ItemColor CurrentColor { get; private set; }
    public bool IsFreezed { get; private set; }
    public Transform HammerParent => _hammerParent;
    public Transform GlassesParent => _glassesParent;

    public event UnityAction<ItemColor> ColorСhanged;
    public event UnityAction BowlHit;


    private void OnDisable()
    {
        _hammer.BowlHit -= OnBowlHit;
        _hammer.BowlIsFreezing -= OnBowlFreezing;
        _game.GameStarted -= OnScreenClosed;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _animator = GetComponent<Animator>();
    }

    public void Init(Game game)
    {
        _game = game;
        _game.GameStarted += OnScreenClosed;
    }

    public void SetHammer(Hammer hammer)
    {
        if (hammer != null)
        {
            if (_hammer != null)
            {
                _hammer.BowlHit -= OnBowlHit;
                _hammer.BowlIsFreezing -= OnBowlFreezing;
            }

            _hammer = hammer;
            _hammer.BowlHit += OnBowlHit;
            _hammer.BowlIsFreezing += OnBowlFreezing;
        }
    }

    public void SetGlasses(Glasses glasses)
    {
        if (_glasses != null)
            Destroy(_glasses.gameObject);

        _glasses = glasses;
    }

    public bool IsAnimationPlaying(string animationName)
    {
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return animatorStateInfo.IsName(animationName);
    }

    public void SetItemColor(ItemColor newColor)
    {
        CurrentColor = newColor;
        _renderer.material.color = CurrentColor.MainColor;
        ColorСhanged?.Invoke(newColor);
    }

    public void HitBowl()
    {
        _animator.SetTrigger(AnimatorDavidController.Params.Hit);
    }

    public void Unfreeze(bool isMadeMoveForAd = false)
    {
        if (isMadeMoveForAd)
            _animator.Play(AnimatorDavidController.States.Idle);
        else
            _animator.SetTrigger(AnimatorDavidController.Params.FreezeOff);

        IsFreezed = false;
    }

    public void Freeze()
    {
        _animator.SetTrigger(AnimatorDavidController.Params.FreezeOn);
        IsFreezed = true;
    }

    public void CelebrateVictory()
    {
        _animator.Play(AnimatorDavidController.States.Victory);
    }

    private void OnScreenClosed()
    {
        _animator.SetTrigger(AnimatorDavidController.Params.GetHammer);
    }

    private void OnBowlHit(IColoredItem colorItem)
    {
        ExchangeColors(colorItem);
        BowlHit?.Invoke();
    }

    private void OnBowlFreezing()
    {
        _freezeActivateEffect.Play();
        Freeze();
    }

    private void ExchangeColors(IColoredItem colorItem)
    {
        ItemColor tempColor = CurrentColor;
        SetItemColor(colorItem.CurrentColor);
        colorItem.SetItemColor(tempColor);
    }
}
