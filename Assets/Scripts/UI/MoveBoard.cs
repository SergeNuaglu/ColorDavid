using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MoveBoard : MonoBehaviour
{
    [SerializeField] private TMP_Text _move;
    [SerializeField] private BowlCircleRotator _circleRotator;
    [SerializeField] private Circle _circle;
    [SerializeField] private MovesHolder _moveHolder;
    [SerializeField] private SDK _sdk;

    private int _currentMoveCount;
    private Coroutine _waitCircleUnlockJob;

    public int MoveCount { get; private set; }

    public event UnityAction MovesCompleted;

    private void OnEnable()
    {
        _circleRotator.MoveDone += OnMoveDone;
        _circle.ColorMismatchFound += OnColorMismatchFound;
    }
    private void OnDisable()
    {
        _circleRotator.MoveDone -= OnMoveDone;
        _circle.ColorMismatchFound -= OnColorMismatchFound;
    }

    private void Start()
    {
        _currentMoveCount = _moveHolder.MoveCount;
        MoveCount = _currentMoveCount;
        ShowMoveCount();
    }

    public void TrySetCurrentMoveCount()
    {
        int minMoveCount = 0;

        if (MoveCount > minMoveCount)
        {
            if (_circle.IsLocked() == false)
            {
                _sdk.ShowVideoForMoveForward();
                _circle.MakeForwardMove();
                _currentMoveCount--;
                MoveCount = _currentMoveCount;
                ShowMoveCount();
            }
            else
            {
                StopCoroutine(_waitCircleUnlockJob);
                _waitCircleUnlockJob = StartCoroutine(WaitForCircleUnlock());
            }
        }
    }

    private void OnMoveDone()
    {
        MoveCount--;
        ShowMoveCount();
    }

    private void OnColorMismatchFound()
    {
        int minMoveCount = 0;

        if (MoveCount == minMoveCount)
            MovesCompleted?.Invoke();
    }

    private void ShowMoveCount()
    {
        int minMoveCount = 0;

        if (MoveCount < minMoveCount)
            MoveCount = minMoveCount;

        _move.text = MoveCount.ToString();
    }

    private IEnumerator WaitForCircleUnlock()
    {
        while (_circle.IsLocked())
            yield return null;

        TrySetCurrentMoveCount();
    }
}
