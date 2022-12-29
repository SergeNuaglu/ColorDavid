using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    [SerializeField] private bool _canCgangeColor;

    private Material _material;
    private Color _currentCcolor;
    private Miner _currentMiner;

    public Color CurrentColor => _currentCcolor;
    public bool CanChangeColor => _canCgangeColor;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    public void OnColorChanged(Color newColor)
    {
        _material.color = newColor;
    }

    public void Init(Color firstColor)
    {
        _material.color = firstColor;
        _currentCcolor = _material.color;
    }

    public void SetMiner(Miner miner)
    {
        if (_currentMiner != null)
        {
            _currentMiner.ColorChanged -= OnColorChanged;
        }

        _currentMiner = miner;
        _currentMiner.ColorChanged += OnColorChanged;
    }

    public void DeleteMiner()
    {
        if (_currentMiner != null)
        {
            _currentMiner.ColorChanged -= OnColorChanged;
        }
    }
}
