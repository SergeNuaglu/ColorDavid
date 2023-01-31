using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "CircleItemData/ColorData", order = 51)]

public class ItemColor : ScriptableObject
{
    [SerializeField] private Color _mainColor;
    [SerializeField] private Color _shadedColor;
    [SerializeField] private bool _canPaint;
    [SerializeField] private bool _canFreeze;

    public Color MainColor => _mainColor;
    public Color ShadedColor => _shadedColor;
    public bool CanPaint => _canPaint;  
    public bool CanFreeze => _canFreeze;
}
