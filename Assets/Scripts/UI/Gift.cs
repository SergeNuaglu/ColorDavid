using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _fullnessDisplay;

    private float _onePercentValue = 0.01f;
    private int _currentFullness;
    private char _percentSign = '%';
    private int _maxValue = 100;
    private const string GiftFullnessKey = nameof(GiftFullnessKey);
    public bool IsFull { get; private set; }

    private void Awake()
    {
        _currentFullness = GetCurrentValue();
        PlayerPrefs.SetInt(GiftFullnessKey, _currentFullness);
        _image.fillAmount = _currentFullness * _onePercentValue;
        _fullnessDisplay.text = _currentFullness.ToString() + _percentSign;

        if (_currentFullness == _maxValue)
            IsFull = true;
        else
            IsFull = false;
    }

    private int GetCurrentValue()
    {
        int valuePerLevel;
        int minValuePerLevel = 17;
        int maxValuePerLevel = 25;

        valuePerLevel = Random.Range(minValuePerLevel, maxValuePerLevel);

        if (PlayerPrefs.HasKey(GiftFullnessKey) == false)
            return valuePerLevel;
        else if (PlayerPrefs.GetInt(GiftFullnessKey) + valuePerLevel <= _maxValue)
            return PlayerPrefs.GetInt(GiftFullnessKey) + valuePerLevel;
        else if (PlayerPrefs.GetInt(GiftFullnessKey) == _maxValue)
            return valuePerLevel;

        return _maxValue;
    }
}
