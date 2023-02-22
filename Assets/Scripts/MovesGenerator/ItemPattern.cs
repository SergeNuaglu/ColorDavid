using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemPattern : MonoBehaviour
{
    protected int StepPosition;
    protected int ColorIndex;

    public int CurrentStepPosition => StepPosition;
    public int CurrentColorIndex => ColorIndex;

    public abstract void ChangeColorIndexes(int newColorIndex);
}
