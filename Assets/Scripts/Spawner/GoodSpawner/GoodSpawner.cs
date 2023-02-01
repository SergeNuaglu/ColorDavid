using System.Collections.Generic;
using UnityEngine;

public abstract class GoodSpawner : MonoBehaviour
{
    [SerializeField] private David _david;

    private List<ShopScreen> _shops = new List<ShopScreen>();

    protected Good SpawnedGood;

    public David David => _david;

    private void OnDisable()
    {
        foreach (var shop in _shops)
            shop.ActiveGoodChanged -= OnActiveGoodChoosed;
    }

    public void Init(ShopScreen shop)
    {
        _shops.Add(shop);
        shop.ActiveGoodChanged += OnActiveGoodChoosed;
    }

    protected abstract void Spawn(Good good);

    private void OnActiveGoodChoosed(Good good)
    {
        if (good != null)
            Spawn(good);
    }
}
