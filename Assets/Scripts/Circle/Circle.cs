using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Circle : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _arcAngle;
    [SerializeField] private int _reward = 70;
    [SerializeField] private Player _player;
    [SerializeField] private MovesHandler _moveHandler;

    private List<Bowl> _bowls = new List<Bowl>();
    private List<Platform> _platforms = new List<Platform>();
    private float _totalAngle = 360f;

    public float Step { get; private set; }
    public float Radius => _radius;
    public float ArcAngle => _arcAngle;
    public IReadOnlyList<Platform> Platforms => _platforms;
    public IReadOnlyList<Bowl> Bowls => _bowls;

    public event UnityAction AllColorsMatched;
    public event UnityAction ColorMismatchFound;
    public event UnityAction ForwardMoveMade;

    public void AddBall(Bowl newBawl)
    {
        _bowls.Add(newBawl);
        _moveHandler.AddColoredItem(newBawl);
        _moveHandler.AddMovableItem(newBawl);
    }

    public void AddPlatform(Platform newPlatform)
    {
        _platforms.Add(newPlatform);
        newPlatform.SetAngleOnCircle();
        _moveHandler.AddColoredItem(newPlatform.David);
        _moveHandler.AddFreezableItem(newPlatform.David);
    }

    public void SetStep(float step)
    {
        Step = step;
    }

    public void MakeForwardMove()
    {
        _moveHandler.MakeMove(Step, Radius, transform.position.y);
        TryAllowHitBowls();
        ForwardMoveMade?.Invoke();
    }

    public bool IsLocked()
    {
        if (_platforms.Count == 0)
            return false;

        foreach (var platform in _platforms)
        {
            if (platform.David.IsAnimationPlaying(AnimatorDavidController.States.Hit))
                return true;
        }

        if (_moveHandler.CheckColorsMatch(Platforms))
        {
            _player.AddMoney(_reward);
            AllColorsMatched?.Invoke();
            return true;
        }
        else
        {
            ColorMismatchFound?.Invoke();
        }

        return false;
    }

    public float GetAngleToFixedPosition()
    {
        float smallerAngle = 360;

        foreach (var platform in _platforms)
        {
            foreach (var bowl in _bowls)
            {
                var angle = GetAngleBetweenBowlAndPlatform(bowl, platform); 

                if (Mathf.Abs(angle) < Mathf.Abs(smallerAngle))
                    smallerAngle = angle;
            }
        }

        return smallerAngle;
    }

    public float GetAngleBetweenBowlAndPlatform(Bowl bowl, Platform platform)
    {
        Vector3 bowlPositionOnCircle;
        Vector3 platformPositionOnCircle;

        bowlPositionOnCircle = bowl.GetPositionOnCircle();
        platformPositionOnCircle = platform.GetPositionOnCircle();
        return Vector3.SignedAngle(bowlPositionOnCircle.normalized, platformPositionOnCircle.normalized, Vector3.up);
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

                if (bowlAngle >= platform.AngleOnCircle - offset && bowlAngle <= platform.AngleOnCircle + offset)
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

