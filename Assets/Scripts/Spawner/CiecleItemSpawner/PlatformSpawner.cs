using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : CircleItemSpawner
{
    [SerializeField] private float _bawlDistance;
    [SerializeField] private float _positionY;
    [SerializeField] private List<ShopScreen> _shops;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private ScreensHandler _screenHandler;

    private OneMoveColorData _platformColorData;
    private OneMoveColorData _davidColorData;
    private Arrangement _platformArrangementData;

    protected override void Awake()
    {
        _platformColorData = SpawnData.PlatformColorData;
        _davidColorData = SpawnData.DavidColorData;
        _platformArrangementData = SpawnData.PlatformArrangement;
        base.Awake();
    }

    protected override void TryInstantiateItem(GameObject template, int stepNumber)
    {
        Vector3 spawnPosition;

        if (_platformArrangementData.Data[stepNumber])
        {
            spawnPosition = GetSpawnPosition(stepNumber, _positionY, _bawlDistance);
            GameObject newItem = Instantiate(template, spawnPosition, Quaternion.identity, transform);

            if (newItem.TryGetComponent<Platform>(out Platform platform))
            {
                platform.Init(Circle);

                if (SpawnData.SecretPlatformArrangement.Data[Counter])
                    platform.BecameSecret();

                platform.SetItemColor(_platformColorData.ItemColors[Counter]);
                platform.David.SetItemColor(_davidColorData.ItemColors[Counter]);
                platform.David.Init(_screenHandler);
                InitGoodSpawners(platform.David);
                _audioManager.Init(platform);
                Circle.AddPlatform(platform);

                if (_bawlDistance < 0)
                {
                    platform.David.transform.rotation *= Quaternion.Euler(0, 180, 0);
                }
            }

            Counter++;
        }
    }

    private void InitGoodSpawners(David david)
    {
        GoodSpawner[] spawners = david.GetComponentsInChildren<GoodSpawner>();

        foreach (var shop in _shops)
            foreach (var spawner in spawners)
                spawner.Init(shop);
    }
}
