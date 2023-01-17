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

    public bool IsLocked()
    {
        if(_platforms.Count == 0)
            return false;

        foreach (var platform in _platforms)
        {
            if (platform.David.IsHitAnimationPlaying())
                return true;
        } 

        return false;
    }

    public Quaternion GetRotationToFixedPosition()
    {
        float smallerAngle = 360;
        float positionX;
        float positionZ;
        Vector3 bowlPositionXZ;
        Vector3 platformPositionXZ;


        foreach (var platform in _platforms)
        {
            foreach (var bowl in _bowls)
            {
                positionX = bowl.transform.position.x - transform.position.x;
                positionZ = bowl.transform.position.z - transform.position.z;
                bowlPositionXZ = new Vector3(positionX, 0, positionZ);
                positionX = platform.transform.position.x - transform.position.x;
                positionZ = platform.transform.position.z - transform.position.z;
                platformPositionXZ = new Vector3(positionX, 0, positionZ);
                var angle = Vector3.SignedAngle(bowlPositionXZ.normalized, platformPositionXZ.normalized, Vector3.up);
                if (Mathf.Abs(angle) < Mathf.Abs(smallerAngle))
                    smallerAngle = angle;
            }
        }

        return Quaternion.Euler(0, smallerAngle, 0);
    }


    public void TryAllowHitBowls()
    {
        float bowlAngle;
        float offset = 10;
        float minValue = 0f;

        foreach (var platform in _platforms)
        {
            foreach (var bowl in _bowls)
            {
                if (platform.David.IsFreezed)
                {
                    platform.David.Unfreeze();
                    break;
                }

                bowlAngle = bowl.GetAngleOnCircle();

                if (bowlAngle >= _totalAngle - offset && bowlAngle <= _totalAngle + offset)
                    bowlAngle = minValue;

                if (bowlAngle  >= platform.AngleOnCircle - offset && bowlAngle <= platform.AngleOnCircle + offset)
                {                
                    if (bowl.CurrentColor.CanPaint)
                    {
                        platform.David.HitBowl();
                        break;
                    }                  
                }
            }
        }
    }
}

