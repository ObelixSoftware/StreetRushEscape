using UnityEngine;

public class PedestrianCrosswalk : MonoBehaviour
{
    public enum Direction { Horizontal, Vertical }

    public float moveSpeed = 1f;
    public Direction crossDirection = Direction.Horizontal;
    public BoxCollider2D walkZone;
    public TrafficLightController trafficLight;

    private Vector2 moveDirection;
    private bool isWalking = false;
    private bool isHit = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private float minLimit;
    private float maxLimit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (walkZone == null || trafficLight == null)
        {
            Debug.LogError($"{gameObject.name}: Missing walkZone or trafficLight reference!");
            enabled = false;
            return;
        }

        Bounds bounds = walkZone.bounds;

        if (crossDirection == Direction.Horizontal)
        {
            minLimit = bounds.min.x;
            maxLimit = bounds.max.x;
            moveDirection = Random.value > 0.5f ? Vector2.right : Vector2.left;
        }
        else
        {
            minLimit = bounds.min.y;
            maxLimit = bounds.max.y;
            moveDirection = Random.value > 0.5f ? Vector2.up : Vector2.down;
        }
    }

    void Update()
    {
        if (isHit) return;

        int lightState = trafficLight.GetLightState();
        isWalking = (lightState == 0); // RED means it's safe for pedestrians

        if (isWalking)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            if (crossDirection == Direction.Horizontal)
            {
                if (transform.position.x < minLimit || transform.position.x > maxLimit)
                    Destroy(gameObject);
            }
            else
            {
                if (transform.position.y < minLimit || transform.position.y > maxLimit)
                    Destroy(gameObject);
            }
        }
    }

    public void OnHitByCar()
    {
        if (isHit) return;

        isHit = true;
        moveDirection = Vector2.zero;
        transform.Rotate(0f, 0f, 90f);

        if (spriteRenderer != null)
            spriteRenderer.color = Color.red;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        Invoke(nameof(DestroyPedestrian), 5f);
    }

    void DestroyPedestrian()
    {
        Destroy(gameObject);
    }
}
