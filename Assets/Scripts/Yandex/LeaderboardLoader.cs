using UnityEngine;
using Agava.YandexGames;
using System;

public class LeaderboardLoader : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LeaderboardScreen _leaderboardScreen;
    [SerializeField] private AuthRequestScreen _authRequestScreen;

    private const string LeaderboardName = "Leaders";
    private const string AnonymousName = "Anonymous";

    public event Action<LeaderboardGetEntriesResponse> PlayersLoaded;

    private void OnEnable()
    {
        _player.ScoreSet += OnScoreSet;
        _authRequestScreen.PersonalDataPermissed += OnPersonalDataPermissed;
    }

    private void OnDisable()
    {
        _player.ScoreSet -= OnScoreSet;
        _authRequestScreen.PersonalDataPermissed -= OnPersonalDataPermissed;
    }

    private void OnPersonalDataPermissed()
    {
        GetPlayerEntries();
    }

    private void OnScoreSet()
    {
        GetPlayerEntries();
    }

    private void GetPlayerEntries()
    {
        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            for (int i = 0; i < result.entries.Length; i++)
            {
                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = AnonymousName;

                bool isUser = result.entries[i].player.uniqueID == _player.UniqueID;

                _leaderboardScreen.AddPlayerView(i + 1, name, result.entries[i].score, isUser);
            }
        });
    }
}

public class LeaderboardPlayer
{
    private string _name;
    private int _score;

    public string Name => _name;
    public int Score => _score;

    public void Init(string name, int score)
    {
        _name = name;
        _score = score;
    }
}
