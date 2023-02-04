using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "LevelData/MoveHolder", order = 51)]

public class MovesHolder : ScriptableObject
{
    [SerializeField] private int _moveCount;
    [SerializeField] private OneMoveColorData[] _bowlMoveColors;
    [SerializeField] private OneMoveColorData[] _davidMoveColors;
    [SerializeField] private Arrangement[] _bowlMoveArrangements;

    public int MoveCount => _moveCount;

    public IReadOnlyList<OneMoveColorData> BowlMoveColors => _bowlMoveColors;
    public IReadOnlyList<OneMoveColorData> DavidMoveColors => _davidMoveColors;
    public IReadOnlyList<Arrangement> BowlMoveArrangements => _bowlMoveArrangements;

    private void OnValidate()
    {
        int nullMoveCount = 1;

        if (_bowlMoveColors.Length != _moveCount + nullMoveCount)
            _bowlMoveColors = new OneMoveColorData[_moveCount+ nullMoveCount];

        if (_bowlMoveArrangements.Length != _moveCount + nullMoveCount)
            _bowlMoveArrangements = new Arrangement[_moveCount + nullMoveCount];
    }
}


