using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private GameObject _effectPrefab;

     private ParticleSystem[] _hitEffects;

    private void Start()
    {       
        _hitEffects = _effectPrefab.GetComponentsInChildren<ParticleSystem>();
    }
    public void PlayEffect(Color color)
    {
        int parentEffectIndex = 0;

        for (int i = 0; i < _hitEffects.Length; i++)
        {
            ChangeEffectColor(color, _hitEffects[i]);
        }

        _hitEffects[parentEffectIndex].Play();
    }

    private void ChangeEffectColor(Color currentColor, ParticleSystem effect)
    {
        var main = effect.main;
        main.startColor = currentColor;
    }
}
