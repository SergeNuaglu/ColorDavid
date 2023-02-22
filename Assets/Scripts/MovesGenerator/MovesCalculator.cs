using System; 
using System.Collections.Generic;
using UnityEngine;

public class MovesCalculator : MonoBehaviour
{
    [SerializeField] private int _stepCount;
    [SerializeField] private int _maxMovesCount;
    [SerializeField] private bool[] _bowlArrangement;
    [SerializeField] private readonly bool[] _platformArrangement;
    [SerializeField] private int[] _bowlColorIndexes;
    [SerializeField] private int[] _davidColorIndexes;
    [SerializeField] private readonly int[] _platformColorIndexes;

    private int _freezeColorIndex = 13;
    private List<PlatformPattern> _platformPatterns = new List<PlatformPattern>();
    private List<BowlPattern> _bowlPatterns = new List<BowlPattern>();
    private List<List<BowlPattern>> _oneMoveWays = new List<List<BowlPattern>>();

    private void OnValidate()
    {
        int verticesCounter = 0;

        TryCorrectLength(_bowlArrangement);
        TryCorrectLength(_platformArrangement);
        TryCorrectLength(_bowlColorIndexes, _bowlArrangement, ref verticesCounter);
        TryCorrectLength(_platformColorIndexes, _platformArrangement, ref verticesCounter);

        if (_davidColorIndexes.Length != _platformColorIndexes.Length)
            Array.Resize(ref _davidColorIndexes, _platformColorIndexes.Length);
    }

    private void Awake()
    {
        PlatformPattern newPlatform;
        BowlPattern newBowl;
        int counter = 0;

        for (int i = 0; i < _platformArrangement.Length; i++)
        {
            if (_platformArrangement[i])
            {
                newPlatform = new PlatformPattern(_platformColorIndexes[counter], _davidColorIndexes[counter], i);
                _platformPatterns.Add(newPlatform);
                ++counter;
            }

            counter = 0;
        }

        for (int i = 0; i < _bowlArrangement.Length; i++)
        {
            if (_bowlArrangement[i])
            {
                newBowl = new BowlPattern(_bowlColorIndexes[counter], i);
                _bowlPatterns.Add(newBowl);
                ++counter;
            }
        }

        SetOneMoveWays();
    }

    private void CalculateMoveWays()
    {
        for (int i = 0; i < _maxMovesCount; i++)
        {
            for (int a = 0; a < _oneMoveWays.Count; a++)
            {
                MakeOneMove(_oneMoveWays[a]);

                for (int b = 0; b < _oneMoveWays.Count; b  ++)
                {
                    MakeOneMove(_oneMoveWays[b]);
                }
            }
        }
    }

    private void SetOneMoveWays()
    {
        List<BowlPattern> oneMoveBowlPatterns;

        for (int i = 0; i < _stepCount; i++)
        {
            if (CanMakeMove())
            {
                oneMoveBowlPatterns = _bowlPatterns;
                _oneMoveWays.Add(oneMoveBowlPatterns);
            }

            MakeOneStep();
        }
    }

    private void MakeOneStep()
    {
        int stepCount = 1;

        foreach (var pattern in _bowlPatterns)
        {
            pattern.SetStepPosition(pattern.CurrentStepPosition + stepCount);
        }
    }

    private void MakeOneMove(List<BowlPattern> oneMoveBowlPatterns)
    {
        for (int b = 0; b < _oneMoveWays.Count; b++)
        {
            foreach (var moveBowlPattern in oneMoveBowlPatterns)
            {
                foreach (var platformPattern in _platformPatterns)
                {
                    if (moveBowlPattern.CurrentStepPosition == platformPattern.CurrentStepPosition)
                    {
                        int tempColorIndex = moveBowlPattern.CurrentColorIndex;
                        moveBowlPattern.ChangeColorIndexes(platformPattern.CurrentColorIndex);
                        platformPattern.ChangeColorIndexes(tempColorIndex);
                    }
                }
            }
        }
    }

    private bool CanMakeMove()
    {
        foreach (var bowlPattern in _bowlPatterns)
        {
            foreach (var platformPattern in _platformPatterns)
            {
                if(bowlPattern.CurrentStepPosition == platformPattern.CurrentStepPosition)
                    return true;
            }
        }

        return false;
    }

    private bool CheckColorMatch()
    {
        int matchCount = 0;

        foreach (var platform in _platformPatterns)
        {
            if (platform.CheckColorIndexesMatched())
                matchCount++;
        }

        return matchCount == _platformPatterns.Count;
    }

    private void TryCorrectLength(bool[] figureVertices)
    {
        if (figureVertices.Length != _stepCount)
            Array.Resize(ref figureVertices, _stepCount);
    }

    private void TryCorrectLength(int[] colorIndexes, bool[] figureVertices, ref int verticesCounter)
    {
        if (verticesCounter != colorIndexes.Length)
        {
            foreach (var vertice in figureVertices)
            {
                if (vertice)
                    verticesCounter++;
            }

            Array.Resize(ref colorIndexes, verticesCounter);
        }
    }
}