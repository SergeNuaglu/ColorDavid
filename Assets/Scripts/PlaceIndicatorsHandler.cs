using System.Collections;
using UnityEngine;

public class PlaceIndicatorsHandler : MonoBehaviour
{
    [SerializeField] private Circle _circle;
    [SerializeField] private BowlCircleRotator _rotator;

    private Coroutine _handleRoutine;

    private void OnEnable()
    {
        _rotator.Rotating += OnRotating;
    }

    private void OnDisable()
    {
        _rotator.Rotating -= OnRotating;
    }

    private void OnRotating()
    {
        _handleRoutine = StartCoroutine(HandleIndicators());
    }

    private void StopIndication()
    {
        foreach (var platform in _circle.Platforms)
        {
            platform.Indicator.TurnOffIndicator();
        }

        StopCoroutine(_handleRoutine);
        _handleRoutine = null;
    }

    private bool IsEnoughSpin()
    {
        float deltaRotation = _rotator.GetDeltaRotation();
        float angleToFixedPosition = _circle.GetAngleToFixedPosition();

        return _rotator.CheckRotationAmount(angleToFixedPosition, deltaRotation);
    }

    private void TurnOffAllIndicators()
    {
        foreach (var platform in _circle.Platforms)
        {
            platform.Indicator.TurnOffIndicator();
        }
    }

    private IEnumerator HandleIndicators()
    {
        float offset = 0.01f;

        while (_rotator.IsDragging)
        {
            var angleToFixedPosition = Mathf.Abs(_circle.GetAngleToFixedPosition());

            foreach (var platform in _circle.Platforms)
            {
                for (int i = 0; i < _circle.Bowls.Count; i++)
                {
                    var angle = Mathf.Abs(_circle.GetAngleBetweenBowlAndPlatform(_circle.Bowls[i], platform));

                    if (angle <= angleToFixedPosition + offset && angle >= angleToFixedPosition - offset)
                    {
                        if (IsEnoughSpin())
                        {
                            if (platform.Indicator.IsIndicatorWorking == false)
                                platform.Indicator.TurnOnIndicator(_circle.Bowls[i].CurrentMainColor);
                            else if (platform.Indicator.CurrentColor != _circle.Bowls[i].CurrentMainColor)
                                platform.Indicator.TurnOnIndicator(_circle.Bowls[i].CurrentMainColor);
                        }
                        else
                        {
                            TurnOffAllIndicators();
                        }

                        break;
                    }

                    if (i == _circle.Bowls.Count - 1 && platform.Indicator.IsIndicatorWorking)
                        platform.Indicator.TurnOffIndicator();
                }
            }

            yield return null;
        }

        StopIndication();
    }
}
