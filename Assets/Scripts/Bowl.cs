using UnityEngine;

public class Bowl : CircleItem
{
    [SerializeField] private ParticleSystem _freezeEffect;

    private void Start()
    {
        TurnToCenter(GetAngleOnCircle());

        if(_freezeEffect != null && CurrentColor.CanFreeze)
            _freezeEffect.Play();
    }
}

 

