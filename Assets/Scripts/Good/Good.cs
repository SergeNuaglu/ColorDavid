using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Good : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isDefault;
    [SerializeField]private bool _isBought;

    private bool _isActive;
    private const string ActiveKey = nameof(ActiveKey);
    private const string BoughtKey = nameof(BoughtKey);

    public bool IsDefault => _isDefault;

    public Sprite Icon => _icon;
    public bool IsBought => _isBought;
    public bool IsActive => _isActive;

    public event UnityAction Bought;
    public event UnityAction ActivityChanged;

    private void Awake()
    {
        _isBought = RestoreConditionData(gameObject.name + BoughtKey); 
        _isActive = RestoreConditionData(gameObject.name + ActiveKey);

        if (_isDefault)
            _isBought = true;

        if (IsBought == false)
            _isActive = false;
    }

    public void Buy()
    {
        _isBought = true;
        Bought?.Invoke();
    }

    public void SwitchActive()
    {
        if (IsActive)
            _isActive = false;
        else
            _isActive = true;

        ActivityChanged?.Invoke();
    }

    private bool RestoreConditionData(string key)
    {
        if (PlayerPrefs.HasKey(key))
            return Convert.ToBoolean(PlayerPrefs.GetInt(gameObject.name + BoughtKey));
        else
            PlayerPrefs.SetInt(gameObject.name + BoughtKey, Convert.ToInt32(IsBought));

        return false;
    }
}
