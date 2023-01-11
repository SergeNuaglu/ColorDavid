using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _arcAngle;
    [SerializeField] private int _maxStepCount;

    private List<Bowl> _bowls = new List<Bowl>();
    private List<Platform> _platforms = new List<Platform>();
    private float _totalAngle = 360f;

    public float Radius => _radius;
    public float ArcAngle => _arcAngle;
    public int MaxStepCount => _maxStepCount;

    private void Awake()
    {
        int minStepCount = 1;

        if (_maxStepCount < minStepCount)
            _maxStepCount = minStepCount;
    }

    public void AddBall(Bowl newBawl)
    {
        _bowls.Add(newBawl);
    }

    public void AddPlatform(Platform newPlatform)
    {
        _platforms.Add(newPlatform);
        newPlatform.SetAngleOnCircle();
    }

    public Quaternion GetRotationToFixedPosition()
    {
        float angle = 0;
        float smallerAngle = 360;
        Vector3 bowlPositionXZ;
        Vector3 platformPositionXZ;

        for (int i = 0; i < _bowls.Count; i++)
        {
            for (int j = 0; j < _platforms.Count; j++)
            {
                bowlPositionXZ = new Vector3(_bowls[i].transform.position.x, 0, _bowls[i].transform.position.z);
                platformPositionXZ = new Vector3(_platforms[j].transform.position.x, 0, _platforms[j].transform.position.z);
                angle = Vector3.SignedAngle(bowlPositionXZ.normalized, platformPositionXZ.normalized,Vector3.up);

                if (Mathf.Abs(angle) < Mathf.Abs(smallerAngle))
                    smallerAngle = angle;
            }
        }

        return Quaternion.Euler(0, smallerAngle, 0);
    }

    public bool IsLocked()
    {
        foreach (var platform in _platforms)
        {
            if (platform.David.IsHitAnimationPlaying())
                return true;
        }

        return false;
    }

    public void TryAllowHitBowls()
    {
        float roundedBowlAngle;
        float minValue = 0f;

        foreach (var platform in _platforms)
        {
            foreach (var bowl in _bowls)
            {
                roundedBowlAngle = Mathf.Round(bowl.GetAngleOnCircle());

                if (roundedBowlAngle == _totalAngle)
                    roundedBowlAngle = minValue;

                if (Mathf.Approximately(platform.AngleOnCircle, roundedBowlAngle) && bowl.CurrentColor.CanPaint)
                { 
                    platform.David.HitBowl();
                    break;
                }
            }
        }
    }
}

