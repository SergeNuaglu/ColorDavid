using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private LastLevelData _lastMoney;
    public int Money { get; private set; } 

    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        Money = _lastMoney.Data;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyGood(int price)
    {
        Money -= price;
        MoneyChanged?.Invoke(Money);
        _lastMoney.Set(Money);
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(money);
        _lastMoney.Set(Money);
    }
}
