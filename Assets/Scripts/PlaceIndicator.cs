using UnityEngine;

public class PlaceIndicator : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    public bool IsIndicatorWorking { get; private set; }
    public Color CurrentColor { get; private set; }

    public void TurnOffIndicator()
    {
        _effect.Stop();
        _effect.Clear();
        IsIndicatorWorking = false;
    }
    public void TurnOnIndicator(Color currentColor)
    {
        if(_effect.isPlaying)
            TurnOffIndicator();

        ChangeColor(currentColor);
        _effect.Play();
        IsIndicatorWorking = true;
    }

    public void ChangeColor(Color newColor)
    {
        var main = _effect.main;
        main.startColor = newColor;
        CurrentColor = newColor;
    }
}
