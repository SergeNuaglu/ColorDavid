using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HomeScreen : Screen
{
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Image _soundImage;
    [SerializeField] private Sprite _soundOnIcon;
    [SerializeField] private Sprite _soundOffIcon;

    public event UnityAction SoundButtonClicked;
    public event UnityAction LeaderboardButtonClicked;

    private void OnEnable()
    {
        _soundButton.onClick.AddListener(OnSoundButtonClick);
        _leaderboardButton.onClick.AddListener(() => OnButtonClicked(LeaderboardButtonClicked));
    }

    private void OnDisable()
    {
        _soundButton.onClick.RemoveListener(OnSoundButtonClick);
        _leaderboardButton.onClick.RemoveAllListeners();
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 1f;
    }

    private void OnSoundButtonClick()
    {
        if (_audioManager.SwitchIsMuted())
            _soundImage.sprite = _soundOffIcon;
        else
            _soundImage.sprite = _soundOnIcon;
    }
}
