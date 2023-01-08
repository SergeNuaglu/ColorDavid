using System.Collections.Generic;
using UnityEngine;

public  class AngleFixator : MonoBehaviour
{
    [SerializeField] private float _bowlCount;

    private List <float> _fixedaAngles = new List<float>();
    private float _cirleAngle = 360;
    private float _step;

    public IReadOnlyList<float> FixedAngles => _fixedaAngles;

    private void Awake()
    {
        _step = _cirleAngle / _bowlCount;

        for (int i = 0; i < _bowlCount; i++)
        {
            _fixedaAngles.Add(i * _step);
        }
    }
}
