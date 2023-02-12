using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public class SDK : MonoBehaviour
{
    [SerializeField] private GiftGenerator _giftGenerator;
    [SerializeField] private WinScreen _winScreen;

    private bool _isVideoForRewardShowed;

    public event Action Initialized;

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

    public void ShowVideoForReward()
    {
        VideoAd.Show(OnAdOpened, null, OnAdClosed);
        _isVideoForRewardShowed = true;
    }
    private void OnAdOpened()
    {
        Time.timeScale = 0;
    }

    private void OnAdClosed()
    {
        Time.timeScale = 1;
    }


    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize(Initialized);
    }


    private void OnGiftChoosing()
    {
        ShowVideoForReward();
    }

    private void OnNextButtonClicked()
    {
        if (_isVideoForRewardShowed == false)
            InterstitialAd.Show();
    }
}
