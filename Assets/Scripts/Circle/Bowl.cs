using UnityEngine;

public class Bowl : CircleItem, IMovable
{
    [SerializeField] private ParticleSystem _freezeEffect;

    private void Start()
    {
        TurnToCenter(GetAngleOnCircle());
    }

    public override void SetItemColor(ItemColor newColor)
    {
        base.SetItemColor(newColor);
        CheckFreezeAnility(newColor);
    }

    public void Move(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    private void CheckFreezeAnility(ItemColor color)
    {
        if(_freezeEffect != null)
        {
            if (color.CanFreeze)
                _freezeEffect.Play();
            else
            {
                _freezeEffect.Stop();
                _freezeEffect.Clear();
            }
        }
    }
}



