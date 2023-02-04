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

    private void Awake()
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
        _stepForwardButton.onClick.RemoveAllListeners();
    }

    protected void OnStepForwardButtonClicked()
    {
        _moveBoard.TrySetCurrentMoveCount();
    }
}