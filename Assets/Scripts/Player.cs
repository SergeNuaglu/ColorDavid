using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private SDK _sdk;

    private int _money;
    private int _score;
    private string _uniqueID;
    private const string MoneyKey = nameof(MoneyKey);
    private const string ScoreKey = nameof(ScoreKey);
    private const string LeaderboardName = "Leaders";

    public int Money => _money;
    public int Score => _score;
    public string UniqueID => _uniqueID;

    public event UnityAction<int> MoneyChanged;
    public event Action ScoreSet;


    private void Awake()
    {
        if (TryRestoreData(MoneyKey, out _money))
            MoneyChanged?.Invoke(Money);

        TryRestoreData(ScoreKey, out _score);

        if (YandexGamesSdk.IsInitialized)
           OnSDKInitialized();
    }

    private void OnEnable()
    {
        _sdk.Initialized += OnSDKInitialized;
    }

    private void OnDisable()
    {
        _sdk.Initialized -= OnSDKInitialized;
    }

    public void BuyGood(int price)
    {
        _money -= price;
        MoneyChanged?.Invoke(Money);
    }

    public void AddMoney(int money)
    {
        _money += money;
        MoneyChanged?.Invoke(_money);
        PlayerPrefs.SetInt(MoneyKey, Money);
    }

    public void AddScore(int score)
    {
        _score += score;
        PlayerPrefs.SetInt(ScoreKey, Score);
    }

    private void OnSDKInitialized()
    {
        PlayerAccount.GetProfileData((result) =>
        {
            _uniqueID = result.uniqueID;         
        });

        Leaderboard.SetScore(LeaderboardName, _score, ScoreSet);
    }

    private bool TryRestoreData(string key, out int data)
    {
        int defaultResult = 0;

        if (PlayerPrefs.HasKey(key))
        {
            data = PlayerPrefs.GetInt(key);
            return true;
        }

        data = defaultResult;
        return false;
    }
}
