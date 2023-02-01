using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CircleItemData/OneMoveColorData", order = 51)]

public class OneMoveColorData : ScriptableObject
{
    [SerializeField] private ItemColor[] _itemColors;

    private int _maxLength;
    public IReadOnlyList<ItemColor> ItemColors => _itemColors;

    private void OnValidate()
    {
        foreach (var color in ItemColors)
        {
            if (color == null)
                throw new UnassignedReferenceException("ItemColor");
        }
    }

    public void SetLenght(int lenght)
    {
        _maxLength = lenght;
        _itemColors = new ItemColor[lenght];
    }
}
