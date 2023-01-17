 using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] private ItemSpawnData _itemSpawnData;
    [SerializeField] private GameObject _itemTemplate;
    [SerializeField] private Circle _circle;

    private float _step;

    protected int Counter;

    protected Circle Circle => _circle;
    protected ItemSpawnData ItemSpawnData => _itemSpawnData;

    private void Start()
    {
        _step = (_circle.ArcAngle * Mathf.Deg2Rad) / _circle.MaxStepCount;
        Spawn();
    }

    protected Vector3 GetSpawnPosition(int stepNumber, float positionY = 0, float radiusOffset = 0)
    {
        float positionX = (_circle.Radius + radiusOffset) * Mathf.Sin(_step * stepNumber);
        float positionZ = (_circle.Radius + radiusOffset) * Mathf.Cos(_step * stepNumber);
        return new Vector3(positionX + _circle.transform.position.x, positionY, positionZ + _circle.transform.position.z);
    }

    protected void TrySetColor(IColoredItem coloredItem, int stepNumber)
    {
        if (stepNumber < _itemSpawnData.ItemColors.Count)
            coloredItem.SetItemColor(_itemSpawnData.ItemColors[stepNumber]);
        else
            coloredItem.SetItemColor(_itemSpawnData.DefaultColor);
    }

    protected void TrySetColor(IColoredItem coloredItem, int stepNumber, ItemSpawnData spawnData)
    {
        if (stepNumber < spawnData.ItemColors.Count)
            coloredItem.SetItemColor(spawnData.ItemColors[stepNumber]);
        else
            coloredItem.SetItemColor(spawnData.DefaultColor);
    }

    protected void Spawn()
    {
        for (int i = 0; i < _circle.MaxStepCount; i++)
        {
            while (Counter < _itemSpawnData.ArrangementData.Count)
            {
                if (_itemSpawnData.ArrangementData[Counter])
                {
                    InstantiateItem(_itemTemplate, i);
                    Counter++;
                    break;
                }
                else
                {
                    Counter++;
                }
            }
        }
    }

    protected abstract void InstantiateItem(GameObject template, int stepNumber);
}
