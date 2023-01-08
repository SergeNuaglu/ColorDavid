using System.Collections.Generic;
using UnityEngine;

public class BowlCircle : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _arcAngle;
    [SerializeField] private AngleFixator _angleFixator;  

    private List<Bowl> _bowls = new List<Bowl>();
    private List<David> _davids = new List<David>();
    private List<float> _fixedAnglesOfBowls = new List<float>();

    public float TotalAngle { get; } = 360f;
    public float Radius => _radius;
    public float BowlArcAngle => _arcAngle;
    public IReadOnlyList<float> FixedAnglesOfBowls => _fixedAnglesOfBowls;
    public IReadOnlyList<David> Davids => _davids;

    public void AddBall(Bowl newBawl)
    {
        _bowls.Add(newBawl);
        FixAngle(newBawl.GetAngleAboutZAxis(TotalAngle));
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
            angleAboutZAxis = _bowls[i].GetAngleAboutZAxis(TotalAngle);

            for (int j = 0; j < FixedAnglesOfBowls.Count; j++)
            {
                angleDifference = angleAboutZAxis - FixedAnglesOfBowls[j];

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
        return _bowls[checkBallIndex].GetAngleAboutZAxis(TotalAngle);
    }

    public void TryAllowHitBowls()
    {
        foreach (var david in _davids)
        {
            foreach (var bowl in _bowls)
            {
                //Debug.Log("DavidAngle: " + david.PositionAngle + " BowlAngle: " + bowl.GetAngleAboutZAxis(TotalAngle));
                //Debug.Log(Mathf.Approximately(david.PositionAngle, bowl.GetAngleAboutZAxis(TotalAngle)));
                if (Mathf.Approximately(david.PositionAngle, bowl.GetAngleAboutZAxis(TotalAngle)) && bowl.CurrentColor.CanPaint)
                {
                    david.HitBowl();
                }
            }
        }
    }

    private void FixAngle(float angle)
    {
        _fixedAnglesOfBowls.Add(angle);
    }
}

