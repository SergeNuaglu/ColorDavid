using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Renderer))]

public class David : MonoBehaviour, IColoredItem
{
    [SerializeField] private ParticleSystem _freezeActivateEffect;
    [SerializeField] private Transform _hammerParent;
    [SerializeField] private Transform _glassesParent;

    private Hammer _hammer;
    private Glasses _glasses;
    private Renderer _renderer;
    private Animator _animator;
    private ScreensHandler _screenHandler;

    public Hammer Hammer => _hammer;
    public ItemColor CurrentColor { get; private set; }
    public bool IsFreezed { get; private set; }
    public Transform HammerParent => _hammerParent;
    public Transform GlassesParent => _glassesParent;
  
    public event UnityAction<Color> ColorExchanging;


    private void OnDisable()
    {
        _hammer.BowlHit -= OnBowlHit;
        _hammer.BowlIsFreezing -= OnBowlFreezing;
        _screenHandler.ScreensClosed -= OnScreenClosed;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _animator = GetComponent<Animator>();
    }

    public void Init(ScreensHandler screenHandler)
    {
        _screenHandler = screenHandler;
        _screenHandler.ScreensClosed += OnScreenClosed;
    }

    public void SetHammer(Hammer hammer)
    {
        if(hammer != null)
        {
            if(_hammer != null)
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
        if(_glasses != null)
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
    }

    public void HitBowl()
    {
        _animator.SetTrigger(AnimatorDavidController.Params.Hit);
    }

    public void Unfreeze()
    {
        _animator.SetTrigger(AnimatorDavidController.Params.FreezeOff);
        IsFreezed = false;
    }

    public void CelebrateVictory()
    {
        _animator.SetTrigger(AnimatorDavidController.Params.Victory);
    }
    private void OnScreenClosed()
    {
        _animator.SetTrigger(AnimatorDavidController.Params.GetHammer);
    }

    private void OnBowlHit(IColoredItem colorItem)
    {
        ColorExchanging?.Invoke(colorItem.CurrentColor.MainColor);
        ExchangeColors(colorItem);
    }

    private void OnBowlFreezing()
    {
        _freezeActivateEffect.Play();
        _animator.SetTrigger(AnimatorDavidController.Params.Freeze);
        IsFreezed = true;
    }

    private void ExchangeColors(IColoredItem colorItem)
    {
        ItemColor tempColor = CurrentColor;
        SetItemColor(colorItem.CurrentColor);
        colorItem.SetItemColor(tempColor);
    }
}
