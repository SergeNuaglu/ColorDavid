using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopScreen : Screen
{
    [SerializeField] private List<Good> _goods;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _addMoneyButton;
    [SerializeField] private Button _otherGoodsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private GoodView _tamplate;
    [SerializeField] private int _precePerOneGood;
    [SerializeField] private Player _player;
    [SerializeField] private ShopScreen _otherGoodsScreen;
    [SerializeField] private ClaimGoodScreen _claimGoodScreen;

    private List<Good> _unpurchasedGoods = new List<Good>();
    private List<Good> _purchasedGoods = new List<Good>();
    private List<GoodView> _views = new List<GoodView>();

    public IReadOnlyList<Good> PurchasedGoods => _purchasedGoods;
    public Good ActiveGood { get; private set; }

    public event UnityAction AddMoneyButtonClicked;
    public event UnityAction ExitButtonClicked;
    public event UnityAction<Good> ActiveGoodChanged;

    private void Awake()
    {
        for (int i = 0; i < _goods.Count; i++)
        {
            AddGoodToScrollView(_goods[i]);

            if (_goods[i].IsBuyed == false)
                _unpurchasedGoods.Add(_goods[i]);
            else
                _purchasedGoods.Add(_goods[i]);

            if (_goods[i].IsActive)
                ActiveGood = _goods[i];
        }
    }

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnSellButtonClicked);
        _otherGoodsButton.onClick.AddListener(OnOtherGoodsButtonClicked);
        _addMoneyButton.onClick.AddListener(() => OnButtonClicked(AddMoneyButtonClicked));
        _exitButton.onClick.AddListener(() => OnButtonClicked(ExitButtonClicked));
        _player.MoneyChanged += OnPlayerMoneyChanged;
        _claimGoodScreen.GoodClamed += OnGoodChoosed;

        foreach (var view in _views)
            view.Choosed += OnGoodChoosed;
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnSellButtonClicked);
        _addMoneyButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
        _player.MoneyChanged -= OnPlayerMoneyChanged;
        _claimGoodScreen.GoodClamed -= OnGoodChoosed;


        foreach (var view in _views)
            view.Choosed += OnGoodChoosed;
    }

    private void Start()
    {
        ActiveGoodChanged?.Invoke(ActiveGood);
    }

    public override void Close()
    {
        base.Close();
        //ActiveGoodChanged?.Invoke(ActiveGood);
    }

    public void GetGift()
    {
        Good good;

        if (_unpurchasedGoods.Count > 0)
        {
            good = GetRandomGood();
            good.Buy();
            _unpurchasedGoods.Remove(good);
            _purchasedGoods.Add(good);
            _claimGoodScreen.ShowGood(good);
        }
    }

    private void OnPlayerMoneyChanged(int money)
    {
        if (money < _precePerOneGood)
            _sellButton.interactable = false;
        else
            _sellButton.interactable = true;
    }

    private void AddGoodToScrollView(Good good)
    {
        GoodView view = Instantiate(_tamplate, _itemContainer.transform);
        view.Init(good);
        view.Render();
        _views.Add(view);
    }

    private void OnSellButtonClicked()
    {
        Good good = GetRandomGood();

        if (_unpurchasedGoods.Count > 0)
            if (TrySellGood(good))
                _claimGoodScreen.ShowGood(good);
    }

    private void OnOtherGoodsButtonClicked()
    {
        _otherGoodsScreen.Open();
        this.Close();
    }

    private void OnGoodChoosed(Good good)
    {
        foreach (var purchasedGood in PurchasedGoods)
        {
            if (purchasedGood.IsActive)
                purchasedGood.SwitchActive();
        }

        good.SwitchActive();
        ActiveGood = good;
        ActiveGoodChanged?.Invoke(ActiveGood);
    }

    private bool TrySellGood(Good good)
    {
        bool isSelled = false;

        if (good != null && _precePerOneGood <= _player.Money)
        {
            _player.BuyGood(_precePerOneGood);
            good.Buy();
            _unpurchasedGoods.Remove(good);
            _purchasedGoods.Add(good);
            isSelled = true;
        }

        return isSelled;
    }

    private Good GetRandomGood()
    {
        float minValue = -1f;

        float random = Random.Range(minValue, _unpurchasedGoods.Count);

        for (int i = 0; i < _unpurchasedGoods.Count; i++)
        {
            if (random <= i)
                return _unpurchasedGoods[i];
        }

        return null;
    }
}

