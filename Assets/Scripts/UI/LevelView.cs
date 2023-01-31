using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelView : ItemView
{
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private LastLevelData _passedLevelNumber;

    private int _levelBuildIndex;
    private int _number;

    public event UnityAction<int> LevelChoosed;

    private void OnEnable()
    {
        ItemButton.onClick.AddListener(OnLevelButtonClicked);
    }

    private void OnDisable()
    {
        ItemButton.onClick.RemoveListener(OnLevelButtonClicked);
    }

    public void Init(int levelBuildIndex, int itemNumber)
    {
        _levelBuildIndex = levelBuildIndex;
        _number = itemNumber;
    }

    public override void Render()
    {
        int levelNumber = _number + 1;

        if (_number <= _passedLevelNumber.Data)
        {
            Lock.Open();
            _levelNumber.text = levelNumber.ToString();
            _levelNumber.color = Color.green;
        }
        else
        {
            ItemButton.interactable = false;
        }

        if (_levelBuildIndex == _number)
            ActivityStateFrame.TurnOn();
        else
            ActivityStateFrame.TurnOff();
    }

    private void OnLevelButtonClicked()
    {
        LevelChoosed?.Invoke(_number);
    }
}
