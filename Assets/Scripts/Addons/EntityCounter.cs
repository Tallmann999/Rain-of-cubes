
public class EntityCounter
{
    public int SpawnedObjects { get; private set; }

    public int CreatedObjects { get; private set; }

    public int ActiveObjects { get; private set; }

    public void Update(int spawned, int created, int active)
    {
        SpawnedObjects = spawned;
        CreatedObjects = created;
        ActiveObjects = active;
    }
}