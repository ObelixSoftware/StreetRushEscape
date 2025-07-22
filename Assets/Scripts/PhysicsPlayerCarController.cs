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

    [Header("Boost")]
    public float boostStrength = 2.0f;
    private bool isBoosting = false;

    [Header("Drift Smoke Spawn Point")]
    public Transform driftSmokeSpawnPoint;

    private bool isDrifting = false;
    private bool isDestroyed = false;

    // Drift smoke timing
    private float driftSmokeTimer = 0f;
    public float driftSmokeInterval = 0.05f; // How often to spawn smoke

    // Movement
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;

    Rigidbody2D rb;
    AudioSource audioSource;
    public GameController gameController;

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

        if (gameController == null)
        {
            gameController = GameObject.FindObjectOfType<GameController>();
            if (gameController == null)
            {
                Debug.LogError("GameController not found in scene! Please assign it in the inspector or add one to the scene.");
            }
        }
    }

    void Update()
    {
        // Nothing here for now
    }

    void FixedUpdate()
    {
        if (isDestroyed) return;

        isDrifting = Input.GetKey(KeyCode.Space);
        isBoosting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        ApplyEngineForce();
        ReduceCarDrift();
        ApplySteering();

        float speedPercent = rb.velocity.magnitude / maxSpeed;
        SoundManager.Instance.UpdateEngineSound(speedPercent);

        // Drifting sound + effect logic
        if (isDrifting && rb.velocity.magnitude > 1f)
        {
            SoundManager.Instance.PlayDrift();

            driftSmokeTimer += Time.fixedDeltaTime;
            if (driftSmokeTimer >= driftSmokeInterval)
            {
                driftSmokeTimer = 0f;

                if (driftSmokeSpawnPoint != null)
                {
                    VisualEffectsManager.Instance.StartDriftSmoke(driftSmokeSpawnPoint.position);
                }
            }
        }
        else
        {
            SoundManager.Instance.StopDrift();
            driftSmokeTimer = 0f; // Reset timer so it doesn't queue smokes
        }
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

        float directionMultiplier = (velocityVsUp >= 0) ? 1f : -1f;

        rotationAngle -= steeringInput * turnFactor * minTurningSpeedFactor * directionMultiplier;
        rb.MoveRotation(rotationAngle);
    }

    void ReduceCarDrift()
    {
        //Controlling variables for gradual transition in and out of drifting
        float targetDriftFactor = isDrifting ? 0.95f : 0.4f;
        float transitionSpeed = 5f;

        //driftFactor = isDrifting ? 0.95f : 0.4f;
        driftFactor = Mathf.Lerp(driftFactor, targetDriftFactor, Time.fixedDeltaTime * transitionSpeed);

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
        if (isDestroyed) return;

        currentHealth -= collisionDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();
        Debug.Log($"Car hit! Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            isDestroyed = true;

            Debug.Log("Car destroyed!");

            SoundManager.Instance.StopDrift();
            SoundManager.Instance.StopEngine();
            SoundManager.Instance.PlayExplosion();

            VisualEffectsManager.Instance.PlayExplosion(transform.position);
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
        if (collision.gameObject.TryGetComponent(out PedestrianWalker pedestrian))
        {
            bool pedestrianKilled = pedestrian.Kill();

            if (pedestrianKilled)
            {
                if (gameController != null)
                    gameController.IncreasePursuit(15f);
                else
                    Debug.LogWarning("gameController is null; cannot IncreasePursuit.");

                SoundManager.Instance.PlayPedestrianHitSound(); // Play pedestrian hit sound
            }
        }

        HandleDamage(collision.gameObject);
    }
}
