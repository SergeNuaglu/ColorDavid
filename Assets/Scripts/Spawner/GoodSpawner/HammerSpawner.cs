public class HammerSpawner : GoodSpawner
{
    private Hammer _newHammer;

    protected override void Spawn(Good good)
    {
        if (good.TryGetComponent<Hammer>(out Hammer hammer))
        {
            if (_newHammer != null)
                Destroy(_newHammer.gameObject);

            _newHammer = Instantiate(hammer, David.HammerParent);
            David.SetHammer(_newHammer);
        }
    }
}
