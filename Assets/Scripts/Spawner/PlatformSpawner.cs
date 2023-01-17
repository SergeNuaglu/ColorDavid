using UnityEngine;

public class PlatformSpawner : Spawner
{
    [SerializeField] private float _bawlDistance;
    [SerializeField] private float _positionY;
    [SerializeField] private ItemSpawnData _davidSpawnData;
    [SerializeField] private Arrangement _secretsArrangement;

    private void Awake()
    {
        if(_secretsArrangement == null)
            _secretsArrangement = new Arrangement();
    }

    protected override void InstantiateItem(GameObject template, int stepNumber)
    {
        Vector3 spawnPosition = GetSpawnPosition(Counter, _positionY, _bawlDistance);
        GameObject newItem = Instantiate(template, spawnPosition, Quaternion.identity, transform);

        if (newItem.TryGetComponent<Platform>(out Platform platform))
        {
            platform.Init(Circle);

            if (stepNumber < _secretsArrangement.Data.Count) 
                if (_secretsArrangement.Data[stepNumber])
                    platform.BecameSecret(ItemSpawnData.DefaultColor);

                TrySetColor(platform, stepNumber);

            Circle.AddPlatform(platform);
            TrySetColor(platform.David, stepNumber, _davidSpawnData);

            if (_bawlDistance < 0)
            {
                platform.David.transform.rotation *= Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
