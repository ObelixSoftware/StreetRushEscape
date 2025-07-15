using UnityEngine;

public class DriftSmokeFade : MonoBehaviour
{
    public float lifetime = 0.6f;
    private float timer = 0f;
    private SpriteRenderer sr;
    private Vector3 startScale;
    public float finalScaleMultiplier = 1.3f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        startScale = transform.localScale;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Fade out alpha
        float t = timer / lifetime;
        float alpha = Mathf.Lerp(1f, 0f, t);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

        // Optional: scale up for smoke effect
        float scaleFactor = Mathf.Lerp(1f, finalScaleMultiplier, t);
        transform.localScale = startScale * scaleFactor;

        // Destroy when time is up
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
