using UnityEngine;

public class PedestrianWalker : MonoBehaviour
{
    public enum MoveDirection { Horizontal, Vertical }
    public MoveDirection direction = MoveDirection.Horizontal;

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
    public PedestrianLightController assignedLight; // Assign the pedestrian light controlling this walker
    public float proximityThreshold = 5f;            // Distance to start obeying the pedestrian light

    private bool isCrossing = false;

    void Start()
    {
        startPos = transform.position;

        if (direction == MoveDirection.Horizontal)
            targetPos = startPos + Vector3.right * moveDistance;
        else
            targetPos = startPos + Vector3.up * moveDistance;

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
        bool lightIsGreen = true; // default true if no assigned light

        if (assignedLight != null)
        {
            float distanceToLight = Vector3.Distance(transform.position, assignedLight.transform.position);
            nearLight = distanceToLight <= proximityThreshold;
            lightIsGreen = assignedLight.IsGreen();
        }

        if (nearLight)
        {
            // Stop moving if pedestrian light is red and pedestrian is near
            if (!lightIsGreen)
            {
                // Not crossing - waiting for green
                return;
            }
        }

        // If green or outside proximity, allow crossing
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

                if (direction == MoveDirection.Horizontal)
                    targetPos = startPos + (movingForward ? Vector3.right : Vector3.left) * moveDistance;
                else
                    targetPos = startPos + (movingForward ? Vector3.up : Vector3.down) * moveDistance;
            }
        }
    }

    public bool Kill()
    {
        if (isDead)
            return false;

        isDead = true;
        walkSpeed = 0;
        respawnTimer = 0f;

        gameObject.layer = LayerMask.NameToLayer("DeadPedestrian");

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && deadSprite != null)
        {
            spriteRenderer.sprite = deadSprite;
        }
        else
        {
            Debug.LogWarning("PedestrianWalker: Missing spriteRenderer or deadSprite on " + gameObject.name);
        }

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
            spriteRenderer.sprite = originalSprite;

        if (col != null)
            col.enabled = true;
    }
}
