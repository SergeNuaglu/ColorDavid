using UnityEngine;
using UnityEngine.Events;

public abstract class Good : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed;
    [SerializeField] private bool _isActive;

    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;
    public bool IsActive => _isActive;

    public event UnityAction Bought;
    public event UnityAction ActivityChanged;

    private void Awake()
    {
        if (IsBuyed != false)
            _isActive = false;
    }

    public void Buy()
    {
        _isBuyed = true;
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
}
