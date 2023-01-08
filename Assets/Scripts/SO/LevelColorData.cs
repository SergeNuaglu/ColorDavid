using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "LevelData/LevelColorData",order = 51)]
public class LevelColorData : ScriptableObject
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private List<bool> _arrangementOfPlatforms;
    [SerializeField] private List<ColorOfItem> _bowlColors;
    [SerializeField] private List<ColorOfItem> _platformColors;
    [SerializeField] private List<ColorOfItem> _davidColors;

    public IReadOnlyList<ColorOfItem> BowlColors => _bowlColors;
    public IReadOnlyList<ColorOfItem> PlatformColors => _platformColors;
    public IReadOnlyList<ColorOfItem> DavidColors => _davidColors;
    public IReadOnlyList<bool> ArrangementOfPlatforms=> _arrangementOfPlatforms;
}
