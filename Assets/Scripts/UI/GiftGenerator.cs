using System.Collections.Generic;
using UnityEngine;

public class GiftGenerator : MonoBehaviour
{
    [SerializeField] private List<ShopScreen> _shops;

    public void ChooseGift()
    {
        float minValue = -1f;

        float random = Random.Range(minValue, _shops.Count);

        for (int i = 0; i < _shops.Count; i++)
        {
            if (random <= i)
            {
                _shops[i].GetGift();
                break;
            }
        }
    }
}
