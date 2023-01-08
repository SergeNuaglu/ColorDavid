using UnityEngine;

public class Platform : ColoredItem
{
    [SerializeField] private ParticleSystem _poolEffect;

    public void CreatePool(Color color)
    {
        var main = _poolEffect.main;
        main.startColor = color;
        _poolEffect.Play();
    }
}
