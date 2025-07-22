using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    [Tooltip("Different pedestrian prefabs with different sprites/behaviors")]
    public GameObject[] pedestrianVariants;

    [Tooltip("How often to spawn pedestrians (seconds)")]
    public float spawnInterval = 5f;

    [Tooltip("Should pedestrians respawn after a delay?")]
    public bool loopSpawning = true;

    private void Start()
    {
        SpawnPedestrian();

        if (loopSpawning)
            InvokeRepeating(nameof(SpawnPedestrian), spawnInterval, spawnInterval);
    }

    void SpawnPedestrian()
    {
        if (pedestrianVariants.Length == 0) return;

        int randomIndex = Random.Range(0, pedestrianVariants.Length);
        GameObject pedestrian = Instantiate(pedestrianVariants[randomIndex], transform.position, Quaternion.identity);
    }
}
