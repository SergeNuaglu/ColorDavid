using System.Collections.Generic;
using UnityEngine;

public class AngleFixator : MonoBehaviour
{
    private List <float> _fixedAngles = new List<float>();

    public IReadOnlyList<float> FixedAngles => _fixedAngles;

    public void FixAngle(float angle)
    {
        _fixedAngles.Add(angle);
    }
}

