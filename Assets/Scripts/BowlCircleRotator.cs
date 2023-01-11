 using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BowlCircleRotator : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Circle _circle;
    [SerializeField] private float _minRotation;
    [SerializeField] private float _speed;

    private Quaternion _rotationY;
    private Coroutine _completeRotationJob;
    private Coroutine _checkCircleFixationJob;
    private bool _canRotate = true;
    private float _lastCircleAngle;
    private float _currentCircleAngle;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastCircleAngle = _circle.transform.eulerAngles.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_canRotate)
        {
            TryStopCoroutine(_checkCircleFixationJob);
            TryStopCoroutine(_completeRotationJob);
            _rotationY = Quaternion.Euler(0, eventData.delta.x * _speed, 0);
            Rotate(_circle.transform.rotation * _rotationY);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_canRotate)
        {
            _canRotate = false;
            _rotationY = _circle.GetRotationToFixedPosition();
            _currentCircleAngle = _circle.transform.eulerAngles.y;
            _completeRotationJob = StartCoroutine(CompleteRotationRutine(_circle.transform.rotation * _rotationY));
        }
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

    private IEnumerator CompleteRotationRutine(Quaternion targetRotation)
    {
        WaitForSeconds waitingTime = new WaitForSeconds(0.5f);
        float deltaAngle;

        while (Mathf.Approximately(_circle.transform.rotation.y, targetRotation.y) == false)
        {
            Rotate(targetRotation);
            yield return null;
        }

        deltaAngle = Mathf.DeltaAngle(_lastCircleAngle, _currentCircleAngle);

        if (Mathf.Abs(deltaAngle) > _minRotation)
        {
            _circle.TryAllowHitBowls();
            yield return waitingTime;
            _checkCircleFixationJob = StartCoroutine(CheckCircleFixation());
        }
        else
        {
            _canRotate = true;
        }
    }

    private IEnumerator CheckCircleFixation()
    {
        while (_circle.IsLocked())
            yield return null;

        _canRotate = true;
    }
}
