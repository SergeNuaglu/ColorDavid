using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScreensHandler : MonoBehaviour
{
    [SerializeField] private Circle _circle;
    [SerializeField] private MoveBoard _moveBoard;
    [SerializeField] private HomeScreen _homeScreen;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private PlayScreen _playScreen;
    [SerializeField] private LevelsScreen _levelsScreen;
    [SerializeField] private ShopScreen _glassesShopScreen;
    [SerializeField] private ShopScreen _hammersShopScreen;

    private int _sceneBuildIndex;
    private int _sceneCount;
    private readonly int _minMoveCount = 0;

    public event UnityAction ScreensClosed;

    private void OnEnable()
    {
        _circle.AllColorsMatched += OnAllColorsMatched;
        _moveBoard.MovesCompleted += OnMovesComleted;
        _winScreen.NextLevelButtonClicked += OnNextButtonClicked;
        _playScreen.HomeButtonClicked += OnHomeButtonClicked;
        _playScreen.RestartButtonClicked += OnRestartButtonClicked;
        _playScreen.StepForwardButtonClicked += OnStepForwardButtonClicked;
        _homeScreen.LevelButtonClicked += OnLevelButtonClicked;
        _homeScreen.ShopButtonClicked += OnShopButtonClicked;
        _homeScreen.ExitButtonClicked += OnExitButtonClicked;
        _levelsScreen.ExitButtonClicked += OnExitButtonClicked;
        _levelsScreen.LevelChoosed += OnLevelChoosed;
        _glassesShopScreen.ExitButtonClicked += OnExitButtonClicked;
        _hammersShopScreen.ExitButtonClicked += OnExitButtonClicked;

    }

    private void OnDisable()
    {
        _circle.AllColorsMatched -= OnAllColorsMatched;
        _moveBoard.MovesCompleted -= OnMovesComleted;
        _playScreen.HomeButtonClicked -= OnHomeButtonClicked;
        _playScreen.RestartButtonClicked -= OnRestartButtonClicked;
        _playScreen.StepForwardButtonClicked -= OnStepForwardButtonClicked;
        _homeScreen.LevelButtonClicked -= OnLevelButtonClicked;
        _homeScreen.ShopButtonClicked -= OnShopButtonClicked;
        _homeScreen.ExitButtonClicked -= OnExitButtonClicked;
        _levelsScreen.ExitButtonClicked -= OnExitButtonClicked;
        _levelsScreen.LevelChoosed -= OnLevelChoosed;
        _glassesShopScreen.ExitButtonClicked -= OnExitButtonClicked;
        _hammersShopScreen.ExitButtonClicked -= OnExitButtonClicked;
    }
    private void Start()
    {
        _sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        _sceneCount = SceneManager.sceneCountInBuildSettings;
        StartGame();
    }

    private void OnAllColorsMatched()
    {
        _winScreen.Open();
    }

    private void OnNextButtonClicked()
    {
        int _nextSceneBuildIndex = _sceneBuildIndex + 1;

        if (_nextSceneBuildIndex < _sceneCount)
            SceneManager.LoadScene(_nextSceneBuildIndex);
        else
            SceneManager.LoadScene(_sceneBuildIndex);
    }

    private void OnHomeButtonClicked()
    {
        if (_moveBoard.MoveCount > _minMoveCount)
        {
            _homeScreen.Open();
            Time.timeScale = 0f;
        }
    }

    private void OnRestartButtonClicked()
    {
        if (_moveBoard.MoveCount > _minMoveCount)
            RestartGame();
    }

    private void OnStepForwardButtonClicked()
    {
        if (_moveBoard.MoveCount > _minMoveCount)
            if (_circle.IsLocked() == false)
                _circle.MakeForwardMove();
    }

    private void OnLevelButtonClicked()
    {
        _levelsScreen.Open();
    }

    private void OnShopButtonClicked()
    {
        _glassesShopScreen.Open();
    }

    private void OnExitButtonClicked()
    {
        StartGame();
    }

    private void OnLevelChoosed(int choosedLevelBuildIndex)
    {
        SceneManager.LoadScene(choosedLevelBuildIndex);
    }

    private void OnMovesComleted()
    {
        RestartGame();
    }

    private void StartGame()
    {
        _homeScreen.Close();
        _winScreen.Close();
        _levelsScreen.Close();
        _glassesShopScreen.Close();
        _hammersShopScreen.Close();
        Time.timeScale = 1f;
        ScreensClosed?.Invoke();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
