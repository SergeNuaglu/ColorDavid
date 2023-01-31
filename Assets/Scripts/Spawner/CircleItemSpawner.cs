  using UnityEngine;

public abstract class CircleItemSpawner : MonoBehaviour
{
    [SerializeField] private SpawnData _spawnData;
    [SerializeField] private GameObject _itemTemplate;
    [SerializeField] private Circle _circle;

    private float _step;

    protected int Counter;

    public Circle Circle => _circle;
    public SpawnData SpawnData => _spawnData;

    protected virtual void Awake()
    {
        _step = (_circle.ArcAngle * Mathf.Deg2Rad) / _spawnData.CircleStepCount;
        Circle.SetStep(_step);
        Spawn();
    }

    protected Vector3 GetSpawnPosition(int circleStepNumber, float positionY = 0, float radiusOffset = 0)
    {
        float positionX = (_circle.Radius + radiusOffset) * Mathf.Sin(_step * circleStepNumber);
        float positionZ = (_circle.Radius + radiusOffset) * Mathf.Cos(_step * circleStepNumber);
        return new Vector3(positionX + _circle.transform.position.x, positionY, positionZ + _circle.transform.position.z);
    }

    protected void Spawn()
    {
        for (int i = 0; i < _spawnData.CircleStepCount; i++)
        {
            TryInstantiateItem(_itemTemplate, i);
        }
    }

    protected abstract void TryInstantiateItem(GameObject template, int stepNumber);
}
