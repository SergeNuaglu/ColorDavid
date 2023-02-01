using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HomeScreen : Screen
{
    [SerializeField] private Button _levelButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Image _soundImage;
    [SerializeField] private Sprite _soundOnIcon;
    [SerializeField] private Sprite _soundOffIcon;

    public event UnityAction LevelButtonClicked;
    public event UnityAction ShopButtonClicked;
    public event UnityAction SoundButtonClicked;
    public event UnityAction ExitButtonClicked;

    private void OnEnable()
    {
        _levelButton.onClick.AddListener(() => OnButtonClicked(LevelButtonClicked));
        _shopButton.onClick.AddListener(() => OnButtonClicked(ShopButtonClicked));
        _soundButton.onClick.AddListener(OnSoundButtonClick);
        _exitButton.onClick.AddListener(() => OnButtonClicked(ExitButtonClicked));
    }

    private void OnDisable()
    {
        _levelButton.onClick.RemoveAllListeners();
        _shopButton.onClick.RemoveAllListeners();
        _soundButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
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
