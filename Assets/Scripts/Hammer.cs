using UnityEngine;
using UnityEngine.Events;

public class Hammer : MonoBehaviour
{
    public event UnityAction<IColoredItem> BowlHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bowl>(out Bowl bowl) && bowl.CurrentColor.CanPaint)
        {
            bowl.HitEffect.PlayEffect(bowl.CurrentMainColor);
            BowlHit?.Invoke(bowl);
        }
    }
}
