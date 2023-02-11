using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GiftGenerator : MonoBehaviour
{
    [SerializeField] private List<Shop> _shops;

    public event UnityAction GiftChoosing;

    public void ChooseGift()
    {
        float minValue = -1f;
        float maxValue = _shops.Count - 1;

        GiftChoosing?.Invoke();
        float random = Random.Range(minValue, maxValue);

        for (int i = 0; i < _shops.Count; i++)
        {
            if (random <= i)
            {
                _shops[i].TryGiveGift();
                break;
            }
        }
    }
}
