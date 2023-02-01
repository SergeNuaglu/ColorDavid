using UnityEngine;
using UnityEngine.Events;

public class Hammer : Good
{
    public event UnityAction<IColoredItem> BowlHit;
    public event UnityAction BowlIsFreezing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bowl>(out Bowl bowl))
        {
            if (bowl.CurrentColor.CanFreeze)
            {               
                BowlIsFreezing?.Invoke();
            }
            else if (bowl.CurrentColor.CanPaint)
            {
                bowl.HitEffect.PlayEffect(bowl.CurrentMainColor);
                BowlHit?.Invoke(bowl);
            }
        }
    }
}
