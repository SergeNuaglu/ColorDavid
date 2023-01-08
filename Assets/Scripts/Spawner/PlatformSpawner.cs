using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformSpawner : Spawner
{
    [SerializeField] private Platform _tamplate;
    [SerializeField] private float _bawlDistance;
    [SerializeField] private float _positionY;
    [SerializeField] private BowlSpawner _bawlSpawner;

    public event UnityAction<Platform, Vector3, Quaternion, float> PlatformSpawned;

    private void OnEnable()
    {
        _bawlSpawner.AllBowlsSpawned += OnAllBallSpawned;
    }

    private void OnDisable()
    {
        _bawlSpawner.AllBowlsSpawned -= OnAllBallSpawned;
    }

    public void OnAllBallSpawned()
    {
        Step = CalculateStep();
        Spawn();
    }

    protected override void Spawn()
    {
        Platform newPlatform;
        Vector3 spawnPosition;
        Quaternion rotation;
        float turnoverAngle = 180;

        for (int i = 0; i < _bawlCircle.AngleFixator.FixedAngles.Count; i++)
        {
            if (i < ColorData.ArrangementOfPlatforms.Count && i < ColorData.PlatformColors.Count)
            {
                if (ColorData.ArrangementOfPlatforms[i])
                {
                    rotation = Quaternion.Euler(0, _bawlCircle.AngleFixator.FixedAngles[i] + turnoverAngle, 0);
                    spawnPosition = GetSpawnPosition(i, _positionY, _bawlDistance);
                    newPlatform = Instantiate(_tamplate, spawnPosition, rotation, transform);
                    newPlatform.SetColor(ColorData.PlatformColors[i]);
                    PlatformSpawned?.Invoke(newPlatform, spawnPosition, rotation,_bawlCircle.AngleFixator.FixedAngles[i]);
                }
            }
        }
    }
}
