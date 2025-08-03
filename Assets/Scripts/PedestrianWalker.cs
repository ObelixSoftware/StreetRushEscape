using UnityEngine;

public class PedestrianWalker : MonoBehaviour
{
    public enum MovementMode { RandomWander, FixedDirection }
    public MovementMode movementMode = MovementMode.RandomWander;

    public enum MoveDirection { Horizontal, Vertical }
    public MoveDirection fixedDirection = MoveDirection.Horizontal;

    [Header("Movement")]
    public float moveSpeed = 1.5f;
    public float changeDirectionInterval = 2f;   // Used only for random mode
    public float wanderRadius = 3f;              // Used only for random mode
    public float moveDistance = 3f;              // Used only for fixed mode

    [Header("Death/Respawn")]
    public Sprite deadSprite;
    private Sprite originalSprite;
    private bool isDead = false;
    private float respawnTimer = 0f;
    private float respawnDelay = 5f;

    private Vector2 startPos;
    private Vector2 moveDirection;
    private float directionTimer;
    private Vector2 fixedTargetPos;
    private bool movingForward = true;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    void Start()
    {
        startPos = transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalSprite = spriteRenderer.sprite;

        col = GetComponent<Collider2D>();

        if (movementMode == MovementMode.FixedDirection)
        {
            // Set initial direction
            SetFixedDirectionTarget();
        }
        else
        {
            PickNewRandomDirection();
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

        if (movementMode == MovementMode.RandomWander)
        {
            directionTimer -= Time.deltaTime;
            if (directionTimer <= 0)
                PickNewRandomDirection();

            Vector2 nextPos = (Vector2)transform.position + moveDirection * moveSpeed * Time.deltaTime;

            if (Vector2.Distance(startPos, nextPos) <= wanderRadius)
            {
                transform.position = nextPos;
            }
            else
            {
                PickNewRandomDirection(forceTowardCenter: true);
            }
        }
        else if (movementMode == MovementMode.FixedDirection)
        {
            Vector2 moveDir = (fixedTargetPos - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(moveDir * moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, fixedTargetPos) < 0.05f)
            {
                movingForward = !movingForward;
                SetFixedDirectionTarget();
            }
        }
    }

    void SetFixedDirectionTarget()
    {
        if (fixedDirection == MoveDirection.Horizontal)
        {
            fixedTargetPos = startPos + (movingForward ? Vector2.right : Vector2.left) * moveDistance;
        }
        else
        {
            fixedTargetPos = startPos + (movingForward ? Vector2.up : Vector2.down) * moveDistance;
        }
    }

    void PickNewRandomDirection(bool forceTowardCenter = false)
    {
        if (forceTowardCenter)
        {
            Vector2 toCenter = (startPos - (Vector2)transform.position).normalized;
            moveDirection = (toCenter + Random.insideUnitCircle * 0.3f).normalized;
        }
        else
        {
            moveDirection = Random.insideUnitCircle.normalized;
        }

        directionTimer = Random.Range(changeDirectionInterval * 0.5f, changeDirectionInterval * 1.5f);
    }

    public bool Kill()
    {
        if (isDead) return false;

        isDead = true;
        moveSpeed = 0;
        moveDirection = Vector2.zero;
        respawnTimer = 0f;

        gameObject.layer = LayerMask.NameToLayer("DeadPedestrian");

        if (spriteRenderer != null && deadSprite != null)
            spriteRenderer.sprite = deadSprite;

        if (col != null)
            col.enabled = false;

        return true;
    }

    private void Respawn()
    {
        isDead = false;
        moveSpeed = 1.5f;
        respawnTimer = 0f;
        gameObject.layer = LayerMask.NameToLayer("Default");

        if (spriteRenderer != null)
            spriteRenderer.sprite = originalSprite;

        if (col != null)
            col.enabled = true;

        if (movementMode == MovementMode.FixedDirection)
            SetFixedDirectionTarget();
        else
            PickNewRandomDirection();
    }
}
