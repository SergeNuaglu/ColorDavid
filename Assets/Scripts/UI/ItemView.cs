using UnityEngine;
using UnityEngine.UI;

public abstract class ItemView : MonoBehaviour
{
    [SerializeField] protected Lock Lock;
    [SerializeField] protected ActiveStateFrame ActivityStateFrame;
    [SerializeField] protected Button ItemButton;

    public abstract void Render();
}