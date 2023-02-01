using UnityEngine;

public class GlassesSpawner : GoodSpawner
{
    private Glasses _newGlasses;

    protected override void Spawn(Good good)
    {
        if (good.TryGetComponent<Glasses>(out Glasses glasses))
        {
            if (_newGlasses != null)
                Destroy(_newGlasses.gameObject);

            _newGlasses = Instantiate(glasses, David.GlassesParent);
        }
    }
}
