using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GoodView : ItemView
{
    [SerializeField] private Image _icon;

    private Good _good;

    public event UnityAction<Good> Choosed;

    private void Awake()
    {
        _icon.enabled = false;
    }

    private void OnEnable()
    {
        ItemButton.onClick.AddListener(OnItemButtonClicked);
    }

    private void OnDisable()
    {
        ItemButton.onClick.RemoveListener(OnItemButtonClicked);
        _good.Bought -= OnGoodBought;
        _good.ActivityChanged -= OnGoodActivityChanged;
    }

    public void Init(Good good)
    {
        _good = good;
        _good.Bought += OnGoodBought;
        _good.ActivityChanged += OnGoodActivityChanged;
    }

    public override void Render()
    {
        if (_good.IsBuyed)
        {
            _icon.enabled = true;
            _icon.sprite = _good.Icon;
            Lock.Open();
            ItemButton.interactable = true;
        }
        else
        {
            ItemButton.interactable = false;
        }

        SetActivityFrame();
    }

    private void OnItemButtonClicked()
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

