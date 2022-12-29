using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hammer : MonoBehaviour
{
    public event UnityAction<Color> Hit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Piece piece))
        {
            if (piece.CanChangeColor)
            {
               Hit?.Invoke(piece.CurrentColor);
            }
        }
    }
}
