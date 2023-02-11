 using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScreen : Screen
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _stepForwardButton;
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private MoveBoard _moveBoard;

    public event UnityAction HomeButtonClicked;
    public event UnityAction RestartButtonClicked;
    public event UnityAction StepForwardButtonClicked;

    protected override void Awake()
    {
        _levelNumber.text = (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(() => OnButtonClicked(HomeButtonClicked));
        _restartButton.onClick.AddListener(() => OnButtonClicked(RestartButtonClicked));
        _stepForwardButton.onClick.AddListener(() => OnButtonClicked(StepForwardButtonClicked));
        _stepForwardButton.onClick.AddListener(OnStepForwardButtonClicked);
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
        _stepForwardButton.onClick.RemoveListener(OnStepForwardButtonClicked);
    }

    public override void Open()
    {
        base.Open();
        Time.timeScale = 1.0f;
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 0f;
    }

    protected void OnStepForwardButtonClicked()
    {
        _moveBoard.TrySetCurrentMoveCount();
    }
}