using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ItemData/SpawnData", order = 51)]
public class ItemSpawnData : ScriptableObject
{
    [SerializeField] private List<bool> _arrangementData;
    [SerializeField] private List<ItemColor> _itemColors;
    [SerializeField] private ItemColor _defaultColor;

    public IReadOnlyList<ItemColor> ItemColors => _itemColors;
    public IReadOnlyList<bool> ArrangementData => _arrangementData;
    public ItemColor DefaultColor => _defaultColor;
}
