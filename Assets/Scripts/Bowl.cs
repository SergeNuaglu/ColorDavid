using UnityEngine;

public class Bowl : CircleItem, IMovable
{
    [SerializeField] private ParticleSystem _freezeEffect;


    private void Start()
    {
        TurnToCenter(GetAngleOnCircle());

        if(_freezeEffect != null && CurrentColor.CanFreeze)
            _freezeEffect.Play();
    }
    public void Move(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}

 

