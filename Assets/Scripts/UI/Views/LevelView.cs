using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelView : View
{
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private Lock _lock;
    [SerializeField] private Button _levelButton;

    private int _levelBuildIndex;
    private int _number;
    private const string PassedLevelKey = nameof(PassedLevelKey);


    public event UnityAction<int> LevelChoosed;

    private void OnEnable()
    {
        _levelButton.onClick.AddListener(OnLevelButtonClicked);
    }

    private void OnDisable()
    {
        _levelButton.onClick.RemoveListener(OnLevelButtonClicked);
    }

    public void Init(int levelBuildIndex, int itemNumber)
    {
        _levelBuildIndex = levelBuildIndex;
        _number = itemNumber;
    }

    public override void Render()
    {
        int levelNumber = _number + 1;

        if (_number <= PlayerPrefs.GetInt(PassedLevelKey))
        {
            _lock.Open();
            _levelNumber.text = levelNumber.ToString();
            _levelNumber.color = Color.green;
        }
        else
        {
            _levelButton.interactable = false;
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
