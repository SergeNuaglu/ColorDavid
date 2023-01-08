using System.Collections.Generic;
using UnityEngine;

public class BowlCircle : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _arcAngle;
    [SerializeField] private AngleFixator _angleFixator;  

    private List<Bowl> _bowls = new List<Bowl>();
    private List<David> _davids = new List<David>();

    public float TotalAngle { get; } = 360f;
    public float Radius => _radius;
    public float BowlArcAngle => _arcAngle;

    public AngleFixator AngleFixator => _angleFixator;
    public IReadOnlyList<David> Davids => _davids;

    public void AddBall(Bowl newBawl)
    {
        _bowls.Add(newBawl);
        _angleFixator.FixAngle(newBawl.GetAngleOnCircle(TotalAngle));
    }

    public void AddDavid(David newDavid)
    {
        _davids.Add(newDavid);
    }

    public Quaternion GetRotationToFixedPosition()
    {
        float angleDifference;
        float smallerDifference = 360f;
        float angleAboutZAxis = 0f;
        uint limit = uint.MaxValue;

        for (int i = 0; i != limit; i = _bowls.Count - (++i))
        {
            limit = uint.MinValue;
            angleAboutZAxis = _bowls[i].GetAngleOnCircle(TotalAngle);

            for (int j = 0; j <_angleFixator.FixedAngles.Count; j++)
            {
                angleDifference = angleAboutZAxis - _angleFixator.FixedAngles[j];

                if (Mathf.Abs(angleDifference) < Mathf.Abs(smallerDifference))
                    smallerDifference = angleDifference;
            }

            if (TotalAngle - angleAboutZAxis < smallerDifference)
                smallerDifference = angleAboutZAxis - TotalAngle;
        }

        return Quaternion.Euler(0, -smallerDifference, 0);
    }

    public bool IsLocked()
    {
        foreach (var david in _davids)
            return david.IsHitAnimationPlaying(); 

        return false;
    }

    public float GetCurrentAngle()
    {
        int checkBallIndex = 0;
        return _bowls[checkBallIndex].GetAngleOnCircle(TotalAngle);
    }

    public void TryAllowHitBowls()
    {
        float roundedAngle;
        float minValue = 0f;

        foreach (var david in _davids)
        {
            foreach (var bowl in _bowls)
            {
                roundedAngle = Mathf.Round(bowl.GetAngleOnCircle(TotalAngle));

                if (roundedAngle == TotalAngle)
                    roundedAngle = minValue;

                if (Mathf.Approximately(david.PositionAngle, roundedAngle) && bowl.CurrentColor.CanPaint)
                {
                    david.HitBowl();
                    break;
                }
            }
        }
    }
}

