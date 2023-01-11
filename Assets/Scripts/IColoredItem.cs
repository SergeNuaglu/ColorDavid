using UnityEngine;

public interface IColoredItem 
{
    public ItemColor CurrentColor { get; }
    public void SetItemColor(ItemColor newColor);
}
