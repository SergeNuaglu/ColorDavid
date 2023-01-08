using UnityEngine;

public class DavidSpawner : Spawner
{
    [SerializeField] private David _tamplate;
    [SerializeField] private float _positionY;
    [SerializeField] private PlatformSpawner _platformSpawner;

    private Vector3 _spawnPosition;
    private Quaternion _rotation;
    private float _positionAngle;
    private Platform _platform;
    private void OnEnable()
    {
        _platformSpawner.PlatformSpawned += OnPlatformSpawned;
    }

    private void OnDisable()
    {
        _platformSpawner.PlatformSpawned -= OnPlatformSpawned;
    }
    protected override void Spawn()
    {
        David newDavid = Instantiate(_tamplate, _spawnPosition, _rotation, transform);
        newDavid.Init(_platform, _positionAngle) ;
        _bawlCircle.AddDavid(newDavid);

        if (_bawlCircle.Davids.Count <= ColorData.DavidColors.Count)
            newDavid.SetColor(ColorData.DavidColors[_bawlCircle.Davids.Count - 1]);
    }

    private void OnPlatformSpawned(Platform platform, Vector3 platformPosition, Quaternion platformRotation, float positionAngle)
    {
        _spawnPosition = new Vector3(platformPosition.x, _positionY, platformPosition.z);
        _rotation = platformRotation;
        _platform = platform;
        _positionAngle = positionAngle;
        Spawn();
    }
}
