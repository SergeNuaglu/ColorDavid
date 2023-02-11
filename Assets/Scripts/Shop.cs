using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private int _pricePerOneGood;
    [SerializeField] private Player _player;

    private Good _defaultGood;
    private List<Good> _unpurchasedGoods = new List<Good>();
    private List<Good> _purchasedGoods = new List<Good>();

    public IReadOnlyList<Good> UnpurchasedGoods => _unpurchasedGoods;
    public int PricePerOneGood => _pricePerOneGood;
    public Good ActiveGood { get; private set; }

    public event UnityAction<Good> ActiveGoodChanged;
    public event UnityAction<Good> GiftGiven;

    public void AddUnpurchasedGood(Good good)
    {
        _unpurchasedGoods.Add(good);
        CheckGoodActivity(good);
    }

    public void AddPurchasedGood(Good good)
    {
        _purchasedGoods.Add((Good)good);

        if (good.IsActive)
        {
            ActiveGood = good;
            ActiveGoodChanged?.Invoke(ActiveGood);
        }

        if (good.IsDefault)
            _defaultGood = good;
    }

    public void MakeDefaultGoodActive()
    {      
        if (_defaultGood != null)
            ChangeActiveGood(_defaultGood);
    }

    public void TryGiveGift()
    {
        Good good;

        if (_unpurchasedGoods.Count > 0)
        {
            good = GetRandomGood();
            good.Buy();
            _unpurchasedGoods.Remove(good);
            _purchasedGoods.Add(good);
            GiftGiven?.Invoke(good);
        }
    }

    public bool TrySellGood(out Good good)
    {
        bool isSelled = false;
        Good selledGood = GetRandomGood();

        if (selledGood != null && _pricePerOneGood <= _player.Money)
        {
            _player.BuyGood(_pricePerOneGood);
            selledGood.Buy();
            _unpurchasedGoods.Remove(selledGood);
            _purchasedGoods.Add(selledGood);
            isSelled = true;
        }

        good = selledGood;
        return isSelled;
    }

    public void ChangeActiveGood(Good good)
    {
        foreach (var purchasedGood in _purchasedGoods)
        {
            if (purchasedGood.IsActive)
                purchasedGood.SwitchActive();
        }

        good.SwitchActive();
        ActiveGood = good;
        ActiveGoodChanged?.Invoke(ActiveGood);
    }

    private void CheckGoodActivity(Good good)
    {
        if (good.IsActive)
            ActiveGood = good;
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
