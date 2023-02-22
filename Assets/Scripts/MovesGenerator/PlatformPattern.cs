using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPattern: ItemPattern
{
    private int _davidColorIndex;

    public PlatformPattern(int colorIndex, int davidColorIndex, int step)
    {
        ColorIndex = colorIndex;
        _davidColorIndex = davidColorIndex;
        StepPosition = step;
    }

    public override void ChangeColorIndexes(int newColorIndex)
    {
        _davidColorIndex = newColorIndex;
    }

    public bool CheckColorIndexesMatched()
    {
        return ColorIndex == _davidColorIndex;
    }
}
