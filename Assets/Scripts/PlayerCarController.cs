using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCarController : MonoBehaviour
{
    public float acceleration = 5f;
    public float maxForwardSpeed = 12f;
    public float maxReverseSpeed = 6f;
    public float steeringSpeed = 200f;

    private float currentSpeed = 0f;
    private float steerInput = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.drag = 1f;
    }

    void Update()
    {
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
        if (Mathf.Abs(currentSpeed) > 0.1f && steerInput != 0f)
        {
            float direction = currentSpeed >= 0 ? 1f : -1f;
            rb.MoveRotation(rb.rotation + steerInput * steeringSpeed * Time.fixedDeltaTime * direction);
        }
        else
        {
            // Snap to nearest 90-degree angle when not turning
            float snappedRotation = Mathf.Round(rb.rotation / 90f) * 90f;
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, snappedRotation, Time.fixedDeltaTime * 5f));
        }

        rb.velocity = transform.up * currentSpeed;
    }
}
