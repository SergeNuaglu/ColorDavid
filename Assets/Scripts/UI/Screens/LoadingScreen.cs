using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingScreen : Screen
{
    [SerializeField] private Animator _loadIconAnimator;
    [SerializeField] private Button _playButton;
    [SerializeField] private Image _loadingIcon;


    private const string LoadFlagName = "IsLoad";

    public event UnityAction PlayButtonClicked;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(() => OnButtonClicked(PlayButtonClicked));
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        _loadIconAnimator.SetBool(LoadFlagName, true);
    }

    public override void Open()
    {
        base.Open();
        _playButton.gameObject.SetActive(false);
    }

    public void StopLoading()
    {
        _loadIconAnimator.SetBool(LoadFlagName, false);
        _loadingIcon.enabled = false;
        _playButton.gameObject.SetActive(true);
    }
}
