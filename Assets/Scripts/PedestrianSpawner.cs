using UnityEngine;
using System.Collections.Generic;

public class PedestrianSpawner : MonoBehaviour
{
    [Tooltip("Different pedestrian prefabs with different sprites/behaviors")]
    public GameObject[] pedestrianVariants;

    [Tooltip("Base time between spawns (seconds)")]
    public float spawnInterval = 5f;

    [Tooltip("Allow spawning in a small random offset around spawner?")]
    public bool randomizePosition = false;

    [Tooltip("Max offset if random position is enabled")]
    public float randomOffsetRadius = 2f;

    [Tooltip("Maximum number of active pedestrians this spawner can have")]
    public int maxActivePedestrians = 5;

    [Tooltip("Should pedestrians respawn repeatedly?")]
    public bool loopSpawning = true;

    private List<GameObject> activePedestrians = new List<GameObject>();

    private void Start()
    {
        if (loopSpawning)
            Invoke(nameof(SpawnLoop), spawnInterval);
    }

    void SpawnLoop()
    {
        if (activePedestrians.Count < maxActivePedestrians)
        {
            SpawnPedestrian();
        }

        if (loopSpawning)
        {
            float nextDelay = spawnInterval + Random.Range(-1f, 1f); // slight variation
            Invoke(nameof(SpawnLoop), Mathf.Max(1f, nextDelay));
        }
    }

    void SpawnPedestrian()
    {
        if (pedestrianVariants.Length == 0) return;

        int randomIndex = Random.Range(0, pedestrianVariants.Length);

        Vector3 spawnPos = transform.position;

        if (randomizePosition)
        {
            Vector2 offset = Random.insideUnitCircle * randomOffsetRadius;
            spawnPos += new Vector3(offset.x, offset.y, 0);
        }

        spawnPos.z = 0f;

        GameObject pedestrian = Instantiate(pedestrianVariants[randomIndex], spawnPos, Quaternion.identity);
        pedestrian.transform.SetParent(transform);

        SpriteRenderer sr = pedestrian.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingLayerName = "Default";
            sr.sortingOrder = 5;
        }

        activePedestrians.Add(pedestrian);

        // Auto-remove from list if destroyed (e.g., by respawn or kill)
        PedestrianWalker walker = pedestrian.GetComponent<PedestrianWalker>();
        if (walker != null)
            StartCoroutine(RemoveWhenDead(walker));

        Debug.Log($"Spawned pedestrian: {pedestrian.name} at {spawnPos}");
    }

    System.Collections.IEnumerator RemoveWhenDead(PedestrianWalker walker)
    {
        while (walker != null && walker.gameObject.activeInHierarchy)
        {
            yield return null;
        }

        activePedestrians.RemoveAll(p => p == null || !p.activeInHierarchy);
    }
}
