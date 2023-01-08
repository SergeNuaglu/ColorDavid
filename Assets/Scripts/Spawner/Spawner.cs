using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected LevelColorData ColorData; 
    [SerializeField] protected BowlCircle _bawlCircle;

    protected float Step;

    protected float CalculateStep()
    {
        return (_bawlCircle.BowlArcAngle * Mathf.Deg2Rad) / ColorData.BowlColors.Count;
    }

    protected Vector3 GetSpawnPosition(int stepCount, float positionY = 0, float offset = 0)
    {
        float positionX = (_bawlCircle.Radius + offset) * Mathf.Sin(Step * stepCount);
        float positionZ = (_bawlCircle.Radius + offset) * Mathf.Cos(Step * stepCount);

        return new Vector3(positionX, positionY, positionZ);
    }
    protected abstract void Spawn();
}
