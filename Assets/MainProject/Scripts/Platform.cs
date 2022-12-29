using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Miner _miner;
    [SerializeField] private SwapButton _swipeButton;

    public bool IsWork { get; private set; }

    private void OnEnable()
    {
        _swipeButton.SwipeStoped += TurnOn;
    }

    private void OnDisable()
    {
        _swipeButton.SwipeStoped -= TurnOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Piece piece))
        {
            if (piece.CurrentColor != Color.white && IsWork)
            {
                piece.SetMiner(_miner);

                if (piece.CanChangeColor)
                {
                    _miner.HitWithHammer();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Piece piece))
        {
            piece.DeleteMiner();
            IsWork = false;
        }
    }

    private void TurnOn()
    {
        IsWork = true;
    }
}
