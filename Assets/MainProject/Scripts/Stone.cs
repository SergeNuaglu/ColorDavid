using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stone : MonoBehaviour
{
    [SerializeField] private int _pieceAmount;
    [SerializeField] private float _radius;
    [SerializeField] private Color[] _colors;
    [SerializeField] private Piece _prefab;

    private Piece[] _pieces;
    private float _stepAngle;
    private Vector3[] _lastPiecesPositions;

    public int AmountPieces => _pieces.Length;
    public float StepAngle => _stepAngle;

    private void Start()
    {
        _pieces = new Piece[_pieceAmount];
        _stepAngle = (360 * Mathf.Deg2Rad) / _pieceAmount;

        for (int i = 0; i < _pieces.Length; i++)
        {
            Vector3 spawnPosition = new Vector3(_radius * Mathf.Sin(_stepAngle * i), 0, _radius * Mathf.Cos(_stepAngle * i));
            Piece newPiece = Instantiate(_prefab,spawnPosition,Quaternion.identity, transform);
            _pieces[i] = newPiece;
            _pieces[i].Init(_colors[Random.Range(0, _colors.Length)]);
        }

        _lastPiecesPositions = GetAllPiecesPositions();
    }

    public Vector3[] GetAllPiecesPositions()
    {
        Vector3[] positions = new Vector3[_pieces.Length];

        for (int i = 0; i < _pieces.Length; i++)
        {
            positions[i] = _pieces[i].transform.position;
        }

        return positions;
    }

    public void BringPiecesToRightPositions(Vector3 []_currentPositions)
    {
        for (int i = 0; i < _lastPiecesPositions.Length-1; i++)
        {
            for (int j = 0; j < _currentPositions.Length; j++)
            {
                if (_currentPositions[j].x > _lastPiecesPositions[i].x && _currentPositions[j].x < _lastPiecesPositions[i+1].x)
                {
                    if (_currentPositions[j].x - _lastPiecesPositions[i].x > _lastPiecesPositions[i + 1].x - _currentPositions[j].x)
                        _currentPositions[j] = _lastPiecesPositions[i + 1];
                    else
                        _currentPositions[j] = _lastPiecesPositions[i];
                }
            }
        }

        for (int i = 0; i < _currentPositions.Length; i++)
        {
            _pieces[i].transform.position = _currentPositions[i];
        }

        _lastPiecesPositions = _currentPositions;

    }

    public void RotateRight()
    {
        transform.rotation *= Quaternion.Euler(0f, 0f, 180f);

    }

    public void RotateLeft()
    {
        transform.rotation *= Quaternion.Euler(0f, 0f, -180f);

    }

    public void RotateForward()
    {
        transform.rotation *= Quaternion.Euler(180f, 0f, 0f);

    }

    public void RotateBack()
    {
        transform.rotation *= Quaternion.Euler(-180f, 0f, 0f);

    }

    public void RotateAround()
    {
        transform.localRotation *= Quaternion.Euler(0f, 45f, 0f);
    }

    public void RotateAroundBack()
    {
        transform.localRotation *= Quaternion.Euler(0f, -45f, 0f);

    }
}
