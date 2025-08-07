using UnityEngine;
using UnityEngine.UI;

public class PhysicsPlayerCarController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float acceleration = 5f;
    public float maxForwardSpeed = 6f;
    public float maxReverseSpeed = 6f;
    public float steeringSpeed = 200f;

    [Header("Drift Settings")]
    public float driftFactor = 0.4f;
    public float highDriftFactor = 0.95f;
    public float lowDriftFactor = 0.4f;
    public float driftTransitionSpeed = 5f;

    [Header("Boost Settings")]
    public float boostMultiplier = 1.5f;
    public float boostDrainRate = 25f;
    public float maxBoost = 100f;
    private float currentBoost;
    public Slider boostBarSlider;

    [Header("Health Settings")]
    public int maxHealth = 100;
    public int collisionDamage = 20;
    private int currentHealth;
    public Slider healthBarSlider;

    [Header("Drift Visuals")]
    public Transform driftSmokeSpawnPoint;
    public float driftSmokeInterval = 0.05f;

    private bool isDrifting = false;
    private bool isDestroyed = false;

    private float currentSpeed = 0f;
    private float steerInput = 0f;
    private float accelInput = 0f;
    private float driftSmokeTimer = 0f;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    public GameController gameController;

    // Bounce control variables
    private bool isBouncing = false;
    private float bounceDuration = 0.15f; // how long bounce velocity lasts
    private float bounceTimer = 0f;
    private Vector2 bounceVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        currentHealth = maxHealth;
        currentBoost = maxBoost;

        if (healthBarSlider != null) healthBarSlider.maxValue = maxHealth;
        if (boostBarSlider != null) boostBarSlider.maxValue = maxBoost;

        UpdateHealthUI();
        UpdateBoostUI();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (isDestroyed) return;

        isDrifting = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (isDestroyed) return;

        if (isBouncing)
        {
            bounceTimer -= Time.fixedDeltaTime;
            if (bounceTimer <= 0f)
            {
                isBouncing = false;
                rb.velocity = Vector2.zero; // stop bounce velocity
                currentSpeed = Mathf.Min(currentSpeed, 1f); // reset speed to slow start after bounce
            }
            // Skip normal controls while bouncing to let bounce velocity act
            return;
        }

        float boostFactor = 1f;
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && currentBoost > 0f)
        {
            boostFactor = boostMultiplier;
            currentBoost -= boostDrainRate * Time.fixedDeltaTime;
            currentBoost = Mathf.Clamp(currentBoost, 0f, maxBoost);
        }

        ApplyEngineForce(boostFactor);
        ApplySteering();
        ApplyDrift();
        UpdateBoostUI();

        float speedPercent = rb.velocity.magnitude / (maxForwardSpeed * boostMultiplier);
        SoundManager.Instance.UpdateEngineSound(speedPercent);

        if (isDrifting && rb.velocity.magnitude > 1f)
        {
            SoundManager.Instance.PlayDrift();
            driftSmokeTimer += Time.fixedDeltaTime;
            if (driftSmokeTimer >= driftSmokeInterval)
            {
                driftSmokeTimer = 0f;
                if (driftSmokeSpawnPoint != null)
                    VisualEffectsManager.Instance.StartDriftSmoke(driftSmokeSpawnPoint.position);
            }
        }
        else
        {
            SoundManager.Instance.StopDrift();
            driftSmokeTimer = 0f;
        }
    }

    void ApplyEngineForce(float boostFactor)
    {
        float targetMaxSpeed = (accelInput > 0 ? maxForwardSpeed : maxReverseSpeed) * boostFactor;
        float accelerationRate = acceleration * Time.fixedDeltaTime;

        if (accelInput > 0)
        {
            if (currentSpeed < targetMaxSpeed)
                currentSpeed += accelerationRate * (1f - (currentSpeed / targetMaxSpeed));
            else
                currentSpeed = targetMaxSpeed;
        }
        else if (accelInput < 0)
        {
            if (currentSpeed > -targetMaxSpeed)
                currentSpeed -= accelerationRate * (1f - (-currentSpeed / targetMaxSpeed));
            else
                currentSpeed = -targetMaxSpeed;
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.fixedDeltaTime * 2f);
        }

        Vector2 forwardVelocity = transform.up * currentSpeed;
        Vector2 sidewaysVelocity = Vector2.Dot(rb.velocity, transform.right) * transform.right;

        rb.velocity = forwardVelocity + sidewaysVelocity;
    }

    void ApplySteering()
    {
        if (Mathf.Abs(currentSpeed) > 0.1f && steerInput != 0f)
        {
            float direction = currentSpeed >= 0 ? 1f : -1f;
            rb.MoveRotation(rb.rotation + steerInput * steeringSpeed * Time.fixedDeltaTime * direction);
        }
        else
        {
            float snapped = Mathf.Round(rb.rotation / 90f) * 90f;
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, snapped, Time.fixedDeltaTime * 5f));
        }
    }

    void ApplyDrift()
    {
        float targetFactor = isDrifting ? highDriftFactor : lowDriftFactor;
        driftFactor = Mathf.Lerp(driftFactor, targetFactor, Time.fixedDeltaTime * driftTransitionSpeed);

        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);
        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steerInput = -inputVector.x; // Flip to correct left/right
        accelInput = inputVector.y;
    }

    void HandleDamage(GameObject other)
    {
        if (isDestroyed) return;

        currentHealth -= collisionDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            isDestroyed = true;
            SoundManager.Instance.StopDrift();
            SoundManager.Instance.StopEngine();
            SoundManager.Instance.PlayExplosion();
            VisualEffectsManager.Instance.PlayExplosion(transform.position);
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PedestrianWalker pedestrian))
        {
            if (pedestrian.Kill())
            {
                gameController.IncreasePursuit(15f);
                SoundManager.Instance.PlayPedestrianHitSound();
            }
        }

        HandleDamage(collision.gameObject);

        if (!isBouncing && collision.contacts.Length > 0)
        {
            // Apply a short bounce-back velocity opposite to collision normal with reduced speed
            bounceVelocity = collision.contacts[0].normal * 2f; // slower bounce back
            rb.velocity = bounceVelocity;
            isBouncing = true;
            bounceTimer = bounceDuration;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BoostItem"))
        {
            currentBoost = Mathf.Clamp(currentBoost + 30f, 0, maxBoost);
            UpdateBoostUI();
            Destroy(other.transform.root.gameObject);
        }

        if (other.CompareTag("HealthItem"))
        {
            currentHealth = Mathf.Clamp(currentHealth + 25, 0, maxHealth);
            UpdateHealthUI();
            Destroy(other.transform.root.gameObject);
        }
    }

    void UpdateHealthUI()
    {
        if (healthBarSlider != null)
            healthBarSlider.value = currentHealth;
    }

    void UpdateBoostUI()
    {
        if (boostBarSlider != null)
            boostBarSlider.value = currentBoost;
    }
}
