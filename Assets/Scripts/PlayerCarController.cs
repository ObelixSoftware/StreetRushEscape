using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCarController : MonoBehaviour
{
    public float acceleration = 5f;
    public float maxForwardSpeed = 12f;
    public float maxReverseSpeed = 6f;
    public float steeringSpeed = 200f;
    public float bounceBackDistance = 1f;
    public float bounceBackSpeed = 5f;

    private float currentSpeed = 0f;
    private float steerInput = 0f;
    private Rigidbody2D rb;
    private bool isBouncing = false;
    private Vector2 bounceDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.drag = 1f;
    }

    void Update()
    {
        if (isBouncing) return;

        bool forward = Input.GetKey(KeyCode.Space);
        bool reverse = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (forward)
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxForwardSpeed);
        }
        else if (reverse)
        {
            currentSpeed -= acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxReverseSpeed, 0f);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * 2f);
        }

        steerInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            steerInput = 1f;
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            steerInput = -1f;
    }

    void FixedUpdate()
    {
        if (isBouncing)
        {
            rb.velocity = bounceDirection * bounceBackSpeed;
            return;
        }

        if (Mathf.Abs(currentSpeed) > 0.1f && steerInput != 0f)
        {
            float direction = currentSpeed >= 0 ? 1f : -1f;
            rb.MoveRotation(rb.rotation + steerInput * steeringSpeed * Time.fixedDeltaTime * direction);
        }
        else
        {
            float snappedRotation = Mathf.Round(rb.rotation / 90f) * 90f;
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, snappedRotation, Time.fixedDeltaTime * 5f));
        }

        rb.velocity = transform.up * currentSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBouncing) return;

        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            bounceDirection = contact.normal; // push away from surface
            currentSpeed = 0f;
            isBouncing = true;

            // End bounce after short delay
            Invoke(nameof(StopBounce), 0.2f);
        }
    }

    void StopBounce()
    {
        isBouncing = false;
        rb.velocity = Vector2.zero;
    }
}
