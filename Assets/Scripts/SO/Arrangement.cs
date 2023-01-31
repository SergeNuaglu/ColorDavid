using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArrangementData", menuName = "CircleItemData/ArrangementData", order = 51)]

public class Arrangement : ScriptableObject
{
    [SerializeField] private bool[] _data;

    private int _maxLength;
    public IReadOnlyList<bool> Data => _data;

    public void SetLenght(int length)
    {
        _maxLength = length;
        _data = new bool[length];
    }
}
