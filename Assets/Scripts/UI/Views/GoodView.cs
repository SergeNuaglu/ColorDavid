using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GoodView : View
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _getButton;
    [SerializeField] private Lock _lock;

    private Good _good;

    public event UnityAction<Good> Choosed;

    private void Awake()
    {
        _icon.enabled = false;
    }

    private void OnEnable()
    {
        _getButton.onClick.AddListener(OnGetButtonClicked);
    }

    private void OnDisable()
    {
        _getButton.onClick.RemoveListener(OnGetButtonClicked);
        _good.Bought -= OnGoodBought;
        _good.ActivityChanged -= OnGoodActivityChanged;
    }

    public void Init(Good good)
    {
        _good = good;
        _good.Bought += OnGoodBought;
        _good.ActivityChanged += OnGoodActivityChanged;
        SetActivityFrame();
    }

    public override void Render()
    {
        if (_good.IsBought)
        {
            _icon.enabled = true;
            _icon.sprite = _good.Icon;
            _lock.Open();
            _getButton.interactable = true;
        }
        else
        {
            _getButton.interactable = false;
        }
    }

    private void OnGetButtonClicked()
    {
        Choosed?.Invoke(_good);
    }

    private void OnGoodActivityChanged()
    {
        SetActivityFrame();
    }

    private void SetActivityFrame()
    {
        if (_good.IsActive)
            ActivityStateFrame.TurnOn();
        else
            ActivityStateFrame.TurnOff();
    }

    private void OnGoodBought()
    {
        Render();
    }
}

