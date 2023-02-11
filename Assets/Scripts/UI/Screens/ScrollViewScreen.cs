using UnityEngine;

public abstract class ScrollViewScreen : Screen
{
    [SerializeField] protected View Template;
    [SerializeField] protected GameObject ItemContainer;

    protected abstract void FillScrollView();
}
