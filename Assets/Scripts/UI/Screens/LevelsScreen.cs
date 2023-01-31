using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsScreen : Screen
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private LevelView _template;
    [SerializeField] private GameObject _itemContainer;

    private int _levelCount; private int _currentLevelBuildIndex;
    private List<LevelView> _levelViews = new List<LevelView>();

    public event UnityAction<int> LevelChoosed;
    public event UnityAction ExitButtonClicked;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(() => OnButtonClicked(ExitButtonClicked));
    }

    private void OnDisable()
    {
        foreach (var view in _levelViews)
        {
            view.LevelChoosed -= OnLevelChoosed;
        }

        _exitButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        _levelCount = SceneManager.sceneCountInBuildSettings;
        _currentLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;

        for (int i = 0; i < _levelCount; i++)
        {
            AddLevel(i);
        }

        SubscribeToLevelButtonClick();
    }

    private void AddLevel(int levelNumber)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.Init(_currentLevelBuildIndex, levelNumber);
        view.Render();
        _levelViews.Add(view);
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
