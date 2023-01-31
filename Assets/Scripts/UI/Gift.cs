using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _fullnessDisplay;
    [SerializeField] private LastLevelData _lastFullness;

    private float _onePercentValue = 0.01f;
    private int _currentFullness;
    private char _percentSign = '%';
    private int _maxValue = 100;

    public bool IsFull { get; private set; }

    private void Awake()
    {
        _currentFullness = GetCurrentValue();
        _image.fillAmount= _currentFullness * _onePercentValue;
        _fullnessDisplay.text = _currentFullness.ToString() + _percentSign;

        if (_currentFullness == _maxValue)
            IsFull = true;
        else
            IsFull = false;
    }

    private int GetCurrentValue()
    {
        int valuePerLevel;
        int minValue = 0;
        int minValuePerLevel = 100;
        int maxValuePerLevel = 101;

        valuePerLevel = Random.Range(minValuePerLevel, maxValuePerLevel);

        if (_lastFullness.Data == _maxValue || _lastFullness.Data == minValue)
            return valuePerLevel;
        else if (_lastFullness.Data + valuePerLevel < _maxValue)
            return _lastFullness.Data + valuePerLevel;

        return _maxValue;
    }

    public void SetLastFullness()
    {
        _lastFullness.Set(_currentFullness);
    }
}
