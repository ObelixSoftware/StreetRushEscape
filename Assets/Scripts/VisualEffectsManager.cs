using UnityEngine;

public class VisualEffectsManager : MonoBehaviour
{
    public static VisualEffectsManager Instance;

    public GameObject explosionPrefab;
    public GameObject driftSmokePrefab;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayExplosion(Vector2 position)
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, position, Quaternion.identity);
        }
    }

    public void StartDriftSmoke(Vector3 position)
    {
        if (driftSmokePrefab != null)
        {
            Instantiate(driftSmokePrefab, position, Quaternion.identity);
        }
    }
}
