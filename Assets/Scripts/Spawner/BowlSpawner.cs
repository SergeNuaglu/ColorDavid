using UnityEngine;

public class BowlSpawner : Spawner
{
    protected override void InstantiateItem(GameObject template, int stepNumber)
    {
        GameObject newItem = Instantiate(template, GetSpawnPosition(Counter, transform.position.y), Quaternion.identity, transform);
  
        if(newItem.TryGetComponent<Bowl>(out Bowl bowl))
        {
            bowl.Init(Circle);
            TrySetColor(bowl, stepNumber);
            Circle.AddBall(bowl);
        }
    }
}
