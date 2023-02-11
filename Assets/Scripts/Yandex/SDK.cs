using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public class SDK : MonoBehaviour
{
    [SerializeField] private GiftGenerator _giftGenerator;
    [SerializeField] private WinScreen _winScreen;

    private bool _isVideoForGiftShowed;

    public event Action Initialized;
    public event Action AdClosed;
    public event Action AdOpen;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private void OnEnable()
    {
        _giftGenerator.GiftChoosing += OnGiftChoosing;
        _winScreen.NextLevelButtonClicked += OnNextButtonClicked;
    }

    private void OnDisable()
    {
        _giftGenerator.GiftChoosing -= OnGiftChoosing;
        _winScreen.NextLevelButtonClicked -= OnNextButtonClicked;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize(Initialized);
    }

    public void ShowVideoForMoveForward()
    {
        VideoAd.Show(AdOpen, null, AdClosed);
    }

    private void OnGiftChoosing()
    {
        VideoAd.Show(AdOpen, null, AdClosed);
        _isVideoForGiftShowed = true;
    }

    private void OnNextButtonClicked()
    {
        if (_isVideoForGiftShowed == false)
            InterstitialAd.Show();
    }
}
