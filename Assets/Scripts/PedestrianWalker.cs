using UnityEngine;
using System.Collections;

public class PedestrianWalker : MonoBehaviour
{
    public enum WalkMode { Horizontal, Vertical, Random }
    public WalkMode walkMode = WalkMode.Horizontal;

    public float moveDistance = 3f;
    public float walkSpeed = 1.5f;

    public Sprite deadSprite;
    private Sprite originalSprite;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingForward = true;
    private bool isDead = false;
    private float respawnTimer = 0f;
    private float respawnDelay = 5f;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    [Header("Pedestrian Light Settings")]
    public PedestrianLightController assignedLight;
    public float proximityThreshold = 5f;

    private bool isCrossing = false;

    private float minRespawnDelay = 3f;
    private float maxRespawnDelay = 8f;
    private float fadeInTime = 1f;

    void Start()
    {
        startPos = transform.position;

        if (walkMode == WalkMode.Horizontal)
            targetPos = startPos + Vector3.right * moveDistance;
        else if (walkMode == WalkMode.Vertical)
            targetPos = startPos + Vector3.up * moveDistance;
        else
            PickNewRandomTarget();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
        else
        {
            Debug.LogWarning("PedestrianWalker: SpriteRenderer not found on " + gameObject.name);
        }

        col = GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogWarning("PedestrianWalker: Collider2D not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (isDead)
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer >= respawnDelay)
                Respawn();
            return;
        }

        bool nearLight = false;
        bool lightIsGreen = true;

        if (assignedLight != null)
        {
            float distanceToLight = Vector3.Distance(transform.position, assignedLight.transform.position);
            nearLight = distanceToLight <= proximityThreshold;
            lightIsGreen = assignedLight.IsGreen();
        }

        if (nearLight && !lightIsGreen)
        {
            return;
        }

        if (!isCrossing)
        {
            isCrossing = true;
        }

        if (isCrossing)
        {
            Vector3 moveDir = (targetPos - transform.position).normalized;
            transform.position += moveDir * walkSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPos) < 0.05f)
            {
                movingForward = !movingForward;
                isCrossing = false;

                if (walkMode == WalkMode.Horizontal)
                    targetPos = startPos + (movingForward ? Vector3.right : Vector3.left) * moveDistance;
                else if (walkMode == WalkMode.Vertical)
                    targetPos = startPos + (movingForward ? Vector3.up : Vector3.down) * moveDistance;
                else
                    PickNewRandomTarget();
            }
        }
    }

    void PickNewRandomTarget()
    {
        float range = moveDistance;
        Vector2 randomOffset = Random.insideUnitCircle.normalized * range;
        targetPos = new Vector3(startPos.x + randomOffset.x, startPos.y + randomOffset.y, startPos.z);
    }

    public bool Kill()
    {
        if (isDead)
            return false;

        isDead = true;
        walkSpeed = 0;
        respawnTimer = 0f;
        respawnDelay = Random.Range(minRespawnDelay, maxRespawnDelay);

        gameObject.layer = LayerMask.NameToLayer("DeadPedestrian");

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && deadSprite != null)
            spriteRenderer.sprite = deadSprite;

        if (col != null)
            col.enabled = false;

        return true;
    }

    private void Respawn()
    {
        isDead = false;
        walkSpeed = 1.5f;
        respawnTimer = 0f;

        gameObject.layer = LayerMask.NameToLayer("Default");

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = originalSprite;
            StartCoroutine(FadeIn());
        }

        if (col != null)
            col.enabled = true;

        startPos = transform.position;

        if (walkMode == WalkMode.Horizontal)
            targetPos = startPos + Vector3.right * moveDistance;
        else if (walkMode == WalkMode.Vertical)
            targetPos = startPos + Vector3.up * moveDistance;
        else
            PickNewRandomTarget();
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;
        Color c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;

        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, timer / fadeInTime);
            spriteRenderer.color = c;
            yield return null;
        }

        c.a = 1f;
        spriteRenderer.color = c;
    }
}
