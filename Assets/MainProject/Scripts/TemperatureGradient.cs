using System.Collections.Generic;
using UnityEngine;

public class TemperatureGradient: MonoBehaviour
{
    [SerializeField] private Color[] _colors;

    public static Dictionary<int,Color> TemperatureColors;

    private void Awake()
    {
        for (int i = 0; i < _colors.Length; i++)
        {
            TemperatureColors.Add(i,_colors[i]);
        }
    }
}
