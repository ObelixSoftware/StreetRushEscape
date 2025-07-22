using UnityEngine;

public class PedestrianWalker : MonoBehaviour
{
    public enum MoveDirection { Horizontal, Vertical }
    public MoveDirection direction = MoveDirection.Horizontal;

    public float moveDistance = 3f;   // How far to move from the starting point
    public float moveSpeed = 1.5f;    // Movement speed

    //public GameController controller;

    public Sprite deadSprite;
    private Sprite originalSprite;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingForward = true;
    private bool isDead = false;
    private float respawnTimer = 0f;
    private float respawnDelay = 5f;

    private SpriteRenderer spriteRenderer;
    private new Collider2D collider;  

    void Start()
    {
        startPos = transform.position;

        if (direction == MoveDirection.Horizontal)
            targetPos = startPos + Vector3.right * moveDistance;
        else
            targetPos = startPos + Vector3.up * moveDistance;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;

        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isDead)
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer >= respawnDelay)
            {
                Respawn();
            }
            return;
        }

        Vector3 moveDir = (targetPos - transform.position).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // Check if we've reached the target
        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            // Flip direction
            movingForward = !movingForward;

            if (direction == MoveDirection.Horizontal)
                targetPos = startPos + (movingForward ? Vector3.right : Vector3.left) * moveDistance;
            else
                targetPos = startPos + (movingForward ? Vector3.up : Vector3.down) * moveDistance;
        }
    }

    public bool Kill()
    {
        if (isDead)
            return false;

        isDead = true;
        moveSpeed = 0;
        spriteRenderer.sprite = deadSprite;
        respawnTimer = 0f;

        gameObject.layer = LayerMask.NameToLayer("DeadPedestrian");

        collider.enabled = false;

        return true;
    }

    private void Respawn()
    {
        isDead = false;
        spriteRenderer.sprite = originalSprite;
        moveSpeed = 1.5f;
        respawnTimer = 0f;

        gameObject.layer = LayerMask.NameToLayer("Default");

        collider.enabled = true;
    }
}
