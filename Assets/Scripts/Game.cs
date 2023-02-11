using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Circle _circle;
    [SerializeField] private MoveBoard _moveBoard;
    [SerializeField] private HomeScreen _homeScreen;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private PlayScreen _playScreen;
    [SerializeField] private LevelsScreen _levelsScreen;
    [SerializeField] private ShopScreen _glassesShopScreen;
    [SerializeField] private ShopScreen _hammersShopScreen;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private LeaderboardScreen _leaderBoardScreen;
    [SerializeField] private AuthRequestScreen _authRequestScreen;
    [SerializeField] private SDK _sdk;

    private int _sceneBuildIndex;
    private int _sceneCount;
    private readonly int _minMoveCount = 0;

    public event UnityAction GameStarted;

    private void OnEnable()
    {
        _circle.AllColorsMatched += OnAllColorsMatched;
        _moveBoard.MovesCompleted += OnMovesComleted;
        _winScreen.NextLevelButtonClicked += OnNextButtonClicked;
        _playScreen.HomeButtonClicked += OnHomeButtonClicked;
        _playScreen.RestartButtonClicked += OnRestartButtonClicked;
        _homeScreen.LeaderboardButtonClicked += OnLeaderboardButtonClicked;
        _levelsScreen.LevelChoosed += OnLevelChoosed;
        _loadingScreen.PlayButtonClicked += OnPlayButtonClicked;
        _sdk.Initialized += OnSDKInitialized;
        _sdk.AdOpen += OnAdOpen;
        _sdk.AdClosed += OnAdClosed;
    }

    private void OnDisable()
    {
        _circle.AllColorsMatched -= OnAllColorsMatched;
        _moveBoard.MovesCompleted -= OnMovesComleted;
        _playScreen.HomeButtonClicked -= OnHomeButtonClicked;
        _playScreen.RestartButtonClicked -= OnRestartButtonClicked;
        _homeScreen.LeaderboardButtonClicked -= OnLeaderboardButtonClicked;
        _levelsScreen.LevelChoosed -= OnLevelChoosed;
        _loadingScreen.PlayButtonClicked -= OnPlayButtonClicked;
        _sdk.Initialized -= OnSDKInitialized;
        _sdk.AdOpen -= OnAdOpen;
        _sdk.AdClosed -= OnAdClosed;
    }

    private void Awake()
    {
        _sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        _sceneCount = SceneManager.sceneCountInBuildSettings;

        if (YandexGamesSdk.IsInitialized == false)
            _loadingScreen.Open();
        else
            GameStarted?.Invoke();
    }

    private void OnLeaderboardButtonClicked()
    {
        if (PlayerAccount.IsAuthorized)
            _leaderBoardScreen.Open();
        else
            _authRequestScreen.Open();
    }

    private void OnNextButtonClicked()
    {
        int nextSceneBuildIndex = ++_sceneBuildIndex;
        int firstSceenIndex = 0;

        if (nextSceneBuildIndex < _sceneCount)
            SceneManager.LoadScene(nextSceneBuildIndex);
        else
            SceneManager.LoadScene(firstSceenIndex);
    }

    private void OnHomeButtonClicked()
    {
        if (_moveBoard.MoveCount > _minMoveCount)
        {
            _homeScreen.Open();
            _playScreen.Close();
        }
    }

    private void OnRestartButtonClicked()
    {
        if (_moveBoard.MoveCount > _minMoveCount)
            RestartGame();
    }

    private void StopLoading()
    {
        _loadingScreen.StopLoading();
    }

    private void StartGame()
    {
        GameStarted?.Invoke();
    }

    private void OnAdClosed() => Time.timeScale = 1;

    private void OnAdOpen() => Time.timeScale = 0;

    private void OnSDKInitialized() => StopLoading();

    private void OnPlayButtonClicked() => StartGame();

    private void OnAllColorsMatched() => _winScreen.Open();

    private void OnLevelChoosed(int choosedLevelBuildIndex) => SceneManager.LoadScene(choosedLevelBuildIndex);

    private void OnMovesComleted() => RestartGame();

    private void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
