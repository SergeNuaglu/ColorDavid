using System.Collections.Generic;
using UnityEngine;

public class MovesHandler : MonoBehaviour
{
    [SerializeField] private MovesHolder _moveHolder;

    private List<IColoredItem> _bowls = new List<IColoredItem>();
    private List<IColoredItem> _davids = new List<IColoredItem>();
    private List<IMovable> _movableItems = new List<IMovable>();
    private OneMoveColorData _colorData;

    private int _nextMove = 1;

    public void AddColoredItem(IColoredItem item)
    {
        if (item is Bowl)
            _bowls.Add(item);
        else if (item is David)
            _davids.Add(item);
    }

    public void AddMovableItem(IMovable item)
    {
        _movableItems.Add(item);
    }

    public void MakeMove(float step, float radius, float positionY)
    {
        float positionX;
        float positionZ;
        IReadOnlyList<bool> nextArrangement;
        Vector3 newPosition;
        int counter = 0;

        ChangeColors(_nextMove, _bowls, _moveHolder.BowlMoveColors);
        ChangeColors(_nextMove, _davids, _moveHolder.DavidMoveColors);
        nextArrangement = _moveHolder.BowlMoveArrangements[_nextMove].Data;

        for (int i = 0; i < nextArrangement.Count; i++)
        {
            if (nextArrangement[i])
            {
                positionX = (radius) * Mathf.Sin(step * i);
                positionZ = (radius) * Mathf.Cos(step * i);
                newPosition = new Vector3(positionX + transform.position.x, positionY, positionZ + transform.position.z);
                _movableItems[counter].Move(newPosition);
                counter++;
            }
        }

        _nextMove++;
    }

    public bool CheckColorsMatch(IReadOnlyList<Platform> platforms)
    {
        int matchCounter = 0;

        foreach (var platform in platforms)
        {
            if (platform.CheckColorMatch())
                matchCounter++;
        }

        if (matchCounter == platforms.Count)
            return true;

        return false;
    }

    private void ChangeColors(int nextMoveNumber, List<IColoredItem> coloredItems, IReadOnlyList<OneMoveColorData> moveColorData)
    {
        _colorData = moveColorData[nextMoveNumber];

        for (int i = 0; i < coloredItems.Count; i++)
            coloredItems[i].SetItemColor(_colorData.ItemColors[i]);
    }
}

public interface IMovable
{
    void Move(Vector3 newPosition);
}
