using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ItemData/ItemColor", order = 51)]

public class ColorOfItem : ScriptableObject
{
    [SerializeField] private Color _mainColor;
    [SerializeField] private Color _shadedColor;
    [SerializeField] private bool _canPaint;

    public Color MainColor => _mainColor;

    public Color ShadedColor => _shadedColor;

    public bool CanPaint => _canPaint;  
}
