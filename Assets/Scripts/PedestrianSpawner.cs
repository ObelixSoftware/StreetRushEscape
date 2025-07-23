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

        // Force Z = 0
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0f);

        GameObject pedestrian = Instantiate(pedestrianVariants[randomIndex], spawnPosition, Quaternion.identity);

        // Parent to spawner (optional, helps with scene organization)
        pedestrian.transform.SetParent(transform);

        // Optional: ensure it's visible by correcting sorting layer/order
        SpriteRenderer sr = pedestrian.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingLayerName = "Default"; // Or "Characters" if you have that layer
            sr.sortingOrder = 5;
        }

        Debug.Log($"Spawned pedestrian: {pedestrian.name} at {spawnPosition}");
    }
}
