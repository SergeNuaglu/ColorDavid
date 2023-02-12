using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : ScrollViewScreen
{
    [SerializeField] private List<Good> _goods;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _addMoneyButton;
    [SerializeField] private Player _player;
    [SerializeField] private ClaimGoodScreen _claimGoodScreen;
    [SerializeField] private Shop _shop;
    [SerializeField] private SDK _sdk;

    private List<GoodView> _views = new List<GoodView>();
    private int _rewardForAd = 200;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnSellButtonClicked);
        _addMoneyButton.onClick.AddListener(OnAddMoneyButtonClicked);
        _player.MoneyChanged += OnPlayerMoneyChanged;
        _shop.GiftGiven += OnGiftGiven;
        _claimGoodScreen.GoodClamed += OnGoodChoosed;
    }

    private void OnDisable()
    {
        _addMoneyButton.onClick.RemoveListener(OnAddMoneyButtonClicked);
        _player.MoneyChanged -= OnPlayerMoneyChanged;
        _claimGoodScreen.GoodClamed -= OnGoodChoosed;
        _shop.GiftGiven -= OnGiftGiven;

        foreach (var view in _views)
            view.Choosed -= OnGoodChoosed;
    }

    private void Start()
    {
        FillScrollView();

        if (_shop.ActiveGood == null)
            _shop.MakeDefaultGoodActive();

        foreach (var view in _views)
            view.Choosed += OnGoodChoosed;
    }

    private void OnGiftGiven(Good good)
    {
        _claimGoodScreen.ShowGood(good);
    }

    protected override void FillScrollView()
    {
        for (int i = 0; i < _goods.Count; i++)
        {
            AddGoodToScrollView(_goods[i]);

            if (_goods[i].IsBought == false)
                _shop.AddUnpurchasedGood(_goods[i]);
            else
                _shop.AddPurchasedGood(_goods[i]);
        }
    }

    private void OnGoodChoosed(Good good)
    {
        _shop.ChangeActiveGood(good);
    }

    private void OnPlayerMoneyChanged(int money)
    {
        if (money < _shop.PricePerOneGood)
            _sellButton.interactable = false;
        else
            _sellButton.interactable = true;
    }

    private void AddGoodToScrollView(Good good)
    {
        GoodView view;

        if (Template.TryGetComponent<GoodView>(out GoodView goodView))
        {
            view = Instantiate(goodView, ItemContainer.transform);
            view.Init(good);
            view.Render();
            _views.Add(view);
        }
    }

    private void OnSellButtonClicked()
    {
        Good good;

        if (_shop.UnpurchasedGoods.Count > 0)
            if (_shop.TrySellGood(out good))
                _claimGoodScreen.ShowGood(good);
    }

    private void OnAddMoneyButtonClicked()
    {
        _sdk.ShowVideoForReward();
        _player.AddMoney(_rewardForAd);
    }
}

