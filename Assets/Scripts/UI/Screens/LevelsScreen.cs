using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelsScreen : ScrollViewScreen
{
    private int _levelCount;
    private int _currentLevelBuildIndex;
    private List<LevelView> _levelViews = new List<LevelView>();

    public event UnityAction<int> LevelChoosed;
    public event UnityAction ExitButtonClicked;

    private void OnDisable()
    {
        foreach (var view in _levelViews)
        {
            view.LevelChoosed -= OnLevelChoosed;
        }
    }

    private void Start()
    {
        _levelCount = SceneManager.sceneCountInBuildSettings;
        _currentLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;
        FillScrollView();
    }

    protected override void FillScrollView()
    {
        for (int i = 0; i < _levelCount; i++)
        {
            AddLevel(i);
        }

        SubscribeToLevelButtonClick();
    }

    private void AddLevel(int levelNumber)
    {
        var view = Instantiate(Template, ItemContainer.transform);

        if (view.TryGetComponent<LevelView>(out LevelView levelView))
        {
            levelView.Init(_currentLevelBuildIndex, levelNumber);
            levelView.Render();
            _levelViews.Add(levelView);
        }
    }

    private void SubscribeToLevelButtonClick()
    {
        foreach (var view in _levelViews)
        {
            view.LevelChoosed += OnLevelChoosed;
        }
    }

    private void OnLevelChoosed(int choosedLevelBuildIndex)
    {
        LevelChoosed?.Invoke(choosedLevelBuildIndex);
    }
}
