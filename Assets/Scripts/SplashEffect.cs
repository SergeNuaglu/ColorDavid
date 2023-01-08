using UnityEngine;

public class SplashEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _parentSplotter;
    [SerializeField] private ParticleSystem _longSplotter;

    public void Perform(Color currentColor)
    {
        ChangeColor(currentColor,_parentSplotter);
        ChangeColor(currentColor,_longSplotter);
        _parentSplotter.Play();
    }

    private void ChangeColor(Color currentColor, ParticleSystem effect)
    {
        var main = effect.main;
        main.startColor = currentColor;
    }
}
