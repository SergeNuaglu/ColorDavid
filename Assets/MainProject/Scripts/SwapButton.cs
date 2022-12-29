using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SwapButton : MonoBehaviour,IDragHandler, IEndDragHandler
{
    [SerializeField] private float _speedModifier;
    [SerializeField] private Transform _target;
    [SerializeField] private Stone _stone;


    private float _step;
    private Quaternion _rotationY;
    private float _rotationAnglePerSwipe;
    private int _stepAmount;

    public event UnityAction SwipeStoped;

    private void Start()
    {
        _step = 360 / _stone.AmountPieces;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rotationY = Quaternion.Euler(0, eventData.delta.x * _speedModifier, 0);
        _target.transform.rotation *= _rotationY;

        /*_rotationAnglePerSwipe += eventData.delta.x * _speedModifier;

        if (_rotationAnglePerSwipe > _step || _rotationAnglePerSwipe < -_step)
        {
            _stepAmount = (int)(_rotationAnglePerSwipe / _step);
            _rotationAnglePerSwipe = 0;
        }

        _target.transform.localRotation *= Quaternion.Euler(0f, _step * _stepAmount, 0f);
        _stepAmount = 0;*/

        Debug.Log(_step);
        //_stepAmount = (int)(_rotationY.eulerAngles.y / _step);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3[] _currentPiecePositions = _stone.GetAllPiecesPositions();
        _stone.BringPiecesToRightPositions(_currentPiecePositions);
        SwipeStoped?.Invoke();
    }
}
