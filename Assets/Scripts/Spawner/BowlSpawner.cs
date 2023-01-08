
using UnityEngine;
using UnityEngine.Events;

public class BowlSpawner : Spawner
{
    [SerializeField] private Bowl _tamplate;

    public event UnityAction AllBowlsSpawned;
    private void Start()
    {
        Step = CalculateStep();
        Spawn();
    }

    protected override void Spawn()
    {
        Bowl newBowl;
        Quaternion rotation;

        for (int i = 0; i < ColorData.BowlColors.Count; i++)
        {
            newBowl = Instantiate(_tamplate, GetSpawnPosition(i, transform.position.y), Quaternion.identity, transform);
            newBowl.SetColor(ColorData.BowlColors[i]);
            _bawlCircle.AddBall(newBowl);
            rotation = Quaternion.Euler(0, _bawlCircle.FixedAnglesOfBowls[i], 0);
            newBowl.transform.rotation *= rotation;
        }

        AllBowlsSpawned?.Invoke();
    }
}
