using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArrangementData", menuName = "CircleItemData/ArrangementData", order = 51)]

public class Arrangement : ScriptableObject
{
    [SerializeField] private bool[] _data;

    public IReadOnlyList<bool> Data => _data;

    public void SetLenght(int length)
    {
        Array.Resize(ref _data, length);
    }

    public void SetNewData(int index, bool newData)
    {
        _data[index] = newData;
    }
}
