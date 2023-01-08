using UnityEngine;

public class Bowl : ColoredItem
{
    [SerializeField] private SplashEffect _splashEffect;

    public float GetAngleOnCircle(float circleTotalAngle)
    {
        float sin = transform.position.x * Vector3.forward.z - Vector3.forward.x * transform.position.z;
        float cos = transform.position.x * Vector3.forward.x + transform.position.z * Vector3.forward.z;
        float result = Mathf.Atan2(sin, cos) * ((circleTotalAngle/2) / Mathf.PI);

        if (result < 0)
            return circleTotalAngle + result;

        return result;
    }

    public void SplashWithPaint()
    {
        _splashEffect.Perform(CurrentMainColor);
    }
}

 

