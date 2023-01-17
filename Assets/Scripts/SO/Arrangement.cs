using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ItemData/ArrangementData", order = 51)]

public class Arrangement : ScriptableObject
{
    [SerializeField] private List<bool> _data =new List<bool>();

    public IReadOnlyList<bool> Data => _data;
}
