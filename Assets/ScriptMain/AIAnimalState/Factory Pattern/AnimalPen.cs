using UnityEngine;

public class AnimalPen : MonoBehaviour
{
    public Transform spawnPointType1;
    public Transform spawnPointType2;
    public Transform[] wanderPoints;

    public Transform GetRandomSpawnPoint()
    {
        return Random.value < 0.5f ? spawnPointType1 : spawnPointType2;
    }
}
