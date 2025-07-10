using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicsPlayerCarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.4f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;

    [Header("Health")]
    public int maxHealth = 100;
    public int collisionDamage = 20;
    private int currentHealth;
    public Slider healthBarSlider;

    [Header("Explosion")]
    public GameObject explosionPrefab;
    public AudioClip explosionSound;
    private AudioSource audioSource;

    [Header("Boost")]
    public float boostStrength = 2.0f;
    private bool isBoosting = false;

    private bool isDrifting = false;

    // Movement
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBarSlider != null)
            healthBarSlider.maxValue = maxHealth;

        UpdateHealthUI();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Nothing here for now
    }

    void FixedUpdate()
    {
        isDrifting = Input.GetKey(KeyCode.Space);
        isBoosting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        ApplyEngineForce();
        ReduceCarDrift();
        ApplySteering();
    }

    void ApplyEngineForce()
    {
        float boostFactor = isBoosting ? 1.5f : 1.0f;

        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);

        if (accelerationInput == 0)
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime * 3);
        else
            rb.drag = 0;

        if (velocityVsUp > (maxSpeed * boostFactor) && accelerationInput > 0)
            return;

        if (velocityVsUp < (-maxSpeed * boostFactor) * 0.5f && accelerationInput < 0)
            return;

        Vector2 engineForceVector = accelerationFactor * accelerationInput * transform.up * boostFactor;
        rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minTurningSpeedFactor = Mathf.Clamp01(rb.velocity.magnitude / 8);

        if (velocityVsUp < 0)
            steeringInput = -steeringInput;

        rotationAngle -= steeringInput * turnFactor * minTurningSpeedFactor;
        rb.MoveRotation(rotationAngle);
    }

    void ReduceCarDrift()
    {
        driftFactor = isDrifting ? 0.95f : 0.4f;

        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    void HandleDamage(GameObject collidedObject)
    {
        currentHealth -= collisionDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();
        Debug.Log($"Car hit! Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("Car destroyed!");

            if (explosionPrefab != null)
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            if (explosionSound != null)
                audioSource.PlayOneShot(explosionSound);

            gameObject.SetActive(false);
        }
    }

    void UpdateHealthUI()
    {
        if (healthBarSlider != null)
        {
            healthBarSlider.value = currentHealth;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleDamage(collision.gameObject);
    }
}
