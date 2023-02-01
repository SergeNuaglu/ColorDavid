using System.Collections.Generic;
using UnityEngine;

public class MovesHandler : MonoBehaviour
{
    [SerializeField] private MovesHolder _moveHolder;

    private List<IColoredItem> _coloredItems = new List<IColoredItem>();
    private List<IMovable> _movableItems = new List<IMovable>();
    private OneMoveColorData _colorData;

    private int _nextMove = 1;

    public void AddColoredItem(IColoredItem item)
    {
        _coloredItems.Add(item);
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

        ChangeColors(_nextMove);
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
            if (platform.IsSameColorWithDavid)
                matchCounter++;
        }

        if (matchCounter == platforms.Count)
            return true;

        return false;
        ;
    }

    private void ChangeColors(int nextMoveNumber)
    {
        SetColorData(nextMoveNumber);

        for (int i = 0; i < _coloredItems.Count; i++)
            _coloredItems[i].SetItemColor(_colorData.ItemColors[i]);
    }

    private void SetColorData(int nextMoveNumber)
    {
        _colorData = _moveHolder.BowlMoveColors[nextMoveNumber];
    }

}

public interface IMovable
{
    void Move(Vector3 newPosition);
}
