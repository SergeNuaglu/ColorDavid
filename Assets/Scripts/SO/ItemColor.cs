using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ItemData/ItemColor", order = 51)]

public class ItemColor : ScriptableObject
{
    [SerializeField] private Color _mainColor;
    [SerializeField] private Color _shadedColor;
    [SerializeField] private bool _canPaint;
    [SerializeField] private bool _canFreeze;

    public Color MainColor => _mainColor;
    public Color ShadedColor => _shadedColor;
    public Color DefaultColor { get { return Color.white; } set { } }
    public bool CanPaint => _canPaint;  
    public bool CanFreeze => _canFreeze;
}
