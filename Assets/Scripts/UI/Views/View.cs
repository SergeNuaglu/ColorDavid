using UnityEngine;
using UnityEngine.UI;

public abstract class View : MonoBehaviour
{
    [SerializeField] protected ActiveStateFrame ActivityStateFrame;

    public abstract void Render();
}