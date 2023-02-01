using UnityEngine;

[CreateAssetMenu(fileName = "LastLevelData", menuName = "LevelData/LastLevelData", order = 51)]

public class LastLevelData : ScriptableObject
{
    private int _data;

    public int Data => _data;

    public void Set(int data)
    {
        _data = data;
    }
}
