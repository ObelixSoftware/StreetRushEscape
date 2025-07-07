using UnityEngine;

public class PedestrianWander : MonoBehaviour
{
    //public GameController gameController;

    public float moveSpeed = 1f;
    private Vector2 moveDirection;
    private float changeDirectionTime = 2f;
    private float timer;
    private bool isHit = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private GameObject gameController;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        PickRandomDirection();
    }

    void Update()
    {
        if (isHit) return;

        timer += Time.deltaTime;

        if (timer > changeDirectionTime)
        {
            PickRandomDirection();
            timer = 0f;
        }

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void PickRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        moveDirection = new Vector2(x, y).normalized;
    }

    public void OnHitByCar()
    {
        if (isHit) return; // Avoid triggering twice
        isHit = true;

        //Increase Pursuit
        gameController.GetComponent<GameController>().IncreasePursuit(25f);

        // Stop movement
        moveDirection = Vector2.zero;

        // Simulate fall: rotate + color change
        transform.Rotate(0f, 0f, 90f);

        if (spriteRenderer != null)
            spriteRenderer.color = Color.red;

        // Disable collider so car can move through
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        // Destroy after 5 seconds
        Invoke(nameof(DestroyPedestrian), 5f);
    }

    void DestroyPedestrian()
    {
        Destroy(gameObject);
    }
}

