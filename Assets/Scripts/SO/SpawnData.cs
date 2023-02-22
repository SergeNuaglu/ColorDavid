using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "LevelData/SpawnData", order = 51)]

public class SpawnData : ScriptableObject
{
    [SerializeField] private int _circleStepCount;
    [SerializeField] private Arrangement _platformArrangement;
    [SerializeField] private Arrangement _secretPlatformArrangement;
    [SerializeField] private OneMoveColorData _platformColorData;
    [SerializeField] private int _platformCount;
    [SerializeField] private int _bowlCount;
    [SerializeField] private MovesHolder _moveHolder;

    public int CircleStepCount => _circleStepCount;
    public Arrangement PlatformArrangement => _platformArrangement;
    public Arrangement BowlArrangement => _moveHolder.BowlMoveArrangements[0];
    public Arrangement SecretPlatformArrangement => _secretPlatformArrangement;
    public OneMoveColorData PlatformColorData => _platformColorData;
    public OneMoveColorData BowlColorData => _moveHolder.BowlMoveColors[0];
    public OneMoveColorData DavidColorData => _moveHolder.DavidMoveColors[0];
    public int PlatformCount => _platformCount;
    public int BowlCount => _bowlCount;

    private void OnValidate()
    {
        TryCorrectLength(_platformCount, _circleStepCount);
        TryCorrectLength(_bowlCount, _circleStepCount);
        TryCorrectLength(_platformColorData, _platformCount);
        TryCorrectLength(_secretPlatformArrangement, _platformCount);
        TryCorrectLength(_platformArrangement, _circleStepCount);

        foreach (var oneMoveColor in _moveHolder.BowlMoveColors)
            TryCorrectLength(oneMoveColor, _bowlCount);

        foreach (var oneMoveColor in _moveHolder.DavidMoveColors)
            TryCorrectLength(oneMoveColor, _platformCount);

        foreach (var oneMoveArrangement in _moveHolder.BowlMoveArrangements)
            TryCorrectLength(oneMoveArrangement, _circleStepCount);

        foreach (var isFreezedCondition in _moveHolder.DavidIsFreezedConditions)
            TryCorrectLength(isFreezedCondition, _platformCount);
    }

    private void TryCorrectLength(OneMoveColorData colorData, int length)
    {
        if (colorData.ItemColors.Count != length)
            colorData.SetLenght(length);
    }

    private void TryCorrectLength(Arrangement arrangement, int length)
    {
        if (arrangement.Data.Count != length)
            arrangement.SetLenght(length);
    }

    private void TryCorrectLength(int value, int length)
    {
        if (value > length)
            value = length;
    }
}
