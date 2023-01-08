using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Hammer : MonoBehaviour
{
    public event UnityAction<ColoredItem> BowlHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bowl>(out Bowl bowl) && bowl.CurrentColor.CanPaint)
        {
            Debug.Log(bowl.CurrentColor.CanPaint);
            bowl.SplashWithPaint();
            BowlHit?.Invoke(bowl);
        }
    }
}
