using UnityEngine;

public class PlatformSpawner : Spawner
{
    [SerializeField] private float _bawlDistance;
    [SerializeField] private float _positionY;
    [SerializeField] private ItemSpawnData _davidSpawnData;

    protected override void InstantiateItem(GameObject template, int stepNumber)
    {
        Vector3 spawnPosition = GetSpawnPosition(Counter, _positionY, _bawlDistance);
        GameObject newItem = Instantiate(template, spawnPosition, Quaternion.identity, transform);

        if (newItem.TryGetComponent<Platform>(out Platform platform))
        {
            TrySetColor(platform, stepNumber);         
            Circle.AddPlatform(platform);
            TrySetColor(platform.David, stepNumber, _davidSpawnData);
        }
    }
}
