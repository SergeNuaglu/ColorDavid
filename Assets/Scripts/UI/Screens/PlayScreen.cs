using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayScreen : Screen
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _stepForwardButton;
    [SerializeField] private BowlCircleRotator _circleRotator;
    [SerializeField] private LastLevelData _stepCount;

    public event UnityAction HomeButtonClicked;
    public event UnityAction RestartButtonClicked;
    public event UnityAction StepForwardButtonClicked;

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(() => OnButtonClicked(HomeButtonClicked));
        _restartButton.onClick.AddListener(() => OnButtonClicked(RestartButtonClicked));
        _stepForwardButton.onClick.AddListener(() => OnButtonClicked(StepForwardButtonClicked));
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
        _stepForwardButton.onClick.RemoveAllListeners();
    }
}