using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MoveBoard : MonoBehaviour
{
    [SerializeField] private TMP_Text _move;
    [SerializeField] private BowlCircleRotator _circleRotator;
    [SerializeField] private Circle _circle;
    [SerializeField] private MovesHolder _moveHolder;

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
        MoveCount = _moveHolder.MoveCount;
        ShowMoveCount();
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
}
