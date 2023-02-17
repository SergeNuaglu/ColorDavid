using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BowlCircleRotator : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Circle _circle;
    [SerializeField] private float _minRotation;
    [SerializeField] private float _speed;

    private Quaternion _rotationY;
    private Coroutine _completeRotationJob;
    private Coroutine _checkCircleLockingJob;
    private Coroutine _waitingRoutine;
    private bool _canRotate = true;
    private float _circleLastAngle;
    private float _angleToFixedPosition;
    private float _deltaRotation;

    public bool CanRotate => _canRotate;
    public bool IsDragging { get; private set; }

    public event UnityAction MoveDone;
    public event UnityAction Rotating;

    private void OnEnable()
    {
        _circle.AllColorsMatched += OnAllColorsMatched;
        _circle.ForwardMoveMade += OnForwardMoveMade;
    }


    private void OnDisable()
    {
        _circle.AllColorsMatched -= OnAllColorsMatched;
        _circle.ForwardMoveMade -= OnForwardMoveMade;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragging = true;
        _circleLastAngle = _circle.transform.eulerAngles.y;
        Rotating?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_canRotate)
        {
            TryStopCoroutine(_checkCircleLockingJob);
            TryStopCoroutine(_completeRotationJob);
            TryStopCoroutine(_waitingRoutine);
            _rotationY = Quaternion.Euler(0, eventData.delta.x * _speed, 0);
            Rotate(_circle.transform.rotation * _rotationY);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_canRotate)
        {
            IsDragging = false;
            _canRotate = false;
            _angleToFixedPosition = _circle.GetAngleToFixedPosition();
            _deltaRotation = GetDeltaRotation();
            _rotationY = Quaternion.Euler(0, _angleToFixedPosition, 0);
            _completeRotationJob = StartCoroutine(CompleteRotationRutine(_circle.transform.rotation * _rotationY));
        }
    }

    public float GetDeltaRotation()
    {
        float halfCircle = 180;
        float totalCircle = 360;
        float delta = _circle.transform.eulerAngles.y - _circleLastAngle;

        if (delta > halfCircle)
            delta -= totalCircle;
        else if (delta < -halfCircle)
            delta += totalCircle;

        return delta;
    }

    public bool CheckRotationAmount(float angleToFixedPosition, float deltaRotation)
    {
        if (Mathf.Abs(angleToFixedPosition) > _minRotation || Mathf.Abs(deltaRotation) > _minRotation)
            return true;

        return false;
    }

    private void OnForwardMoveMade()
    {
        TryStopCoroutine(_checkCircleLockingJob);
        TryStopCoroutine(_completeRotationJob);
        TryStopCoroutine(_waitingRoutine);
        _canRotate = false;
        _waitingRoutine = StartCoroutine(Wait());
    }


    private void Rotate(Quaternion targetRotation)
    {
        _circle.transform.rotation = Quaternion.Slerp(_circle.transform.rotation, targetRotation, _speed);
    }

    private void TryStopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private void OnAllColorsMatched()
    {
        TryStopCoroutine(_checkCircleLockingJob);
        TryStopCoroutine(_completeRotationJob);
        TryStopCoroutine(_waitingRoutine);
    }


    private IEnumerator CompleteRotationRutine(Quaternion targetRotation)
    {
        while (Mathf.Approximately(_circle.transform.rotation.y, targetRotation.y) == false)
        {
            Rotate(targetRotation);
            yield return null;
        }

        if (CheckRotationAmount(_angleToFixedPosition, _deltaRotation))
        {
            MoveDone?.Invoke();
            _circle.TryAllowHitBowls();
            _waitingRoutine = StartCoroutine(Wait());
        }
        else
        {
            _canRotate = true;
        }
    }

    private IEnumerator Wait()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(0.5f);

        yield return waitingTime;
        _checkCircleLockingJob = StartCoroutine(CheckCircleLocking());
    }

    private IEnumerator CheckCircleLocking()
    {
        while (_circle.IsLocked())
            yield return null;

        _canRotate = true;
    }
}
