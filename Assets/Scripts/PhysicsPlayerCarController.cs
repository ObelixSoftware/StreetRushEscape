using UnityEngine;
using UnityEngine.UI;

public class PhysicsPlayerCarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.4f;
    public float accelerationFactor = 30f;
    public float turnFactor = 3.5f;
    public float baseMaxSpeed = 20f;

    [Header("Health")]
    public int maxHealth = 100;
    public int collisionDamage = 1;
    private int currentHealth;
    public Slider healthBarSlider;

    [Header("Boost")]
    public float boostMultiplier = 1.5f;
    public float boostDrainRate = 25f;
    public float maxBoost = 100f;
    private float currentBoost;
    public Slider boostBarSlider;

    [Header("Drift Smoke Spawn Point")]
    public Transform driftSmokeSpawnPoint;

    private bool isDrifting = false;
    private bool isDestroyed = false;

    private float driftSmokeTimer = 0f;
    public float driftSmokeInterval = 0.05f;

    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;

    Rigidbody2D rb;
    AudioSource audioSource;
    public GameController gameController;

    private bool straightenMode = false; // CTRL straighten toggle
    public float straightenSpeed = 5f;   // How fast to straighten

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentBoost = maxBoost;

        if (healthBarSlider != null)
            healthBarSlider.maxValue = maxHealth;

        if (boostBarSlider != null)
            boostBarSlider.maxValue = maxBoost;

        UpdateHealthUI();
        UpdateBoostUI();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (isDestroyed) return;

        // Combined WASD and Arrow Keys input
        float vertical = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? 1 :
                         Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ? -1 : 0;

        float horizontal = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ? -1 :
                           Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? 1 : 0;

        SetInputVector(new Vector2(horizontal, vertical));

        isDrifting = Input.GetKey(KeyCode.Space);

        // Activate straighten mode when CTRL is pressed
        straightenMode = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        float boostActive = 1f;
        bool boostKeyHeld = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (boostKeyHeld && currentBoost > 0f)
        {
            boostActive = boostMultiplier;
            currentBoost -= boostDrainRate * Time.fixedDeltaTime;
            currentBoost = Mathf.Clamp(currentBoost, 0, maxBoost);
        }

        UpdateBoostUI();

        ApplyEngineForce(boostActive);
        ReduceCarDrift();

        if (straightenMode)
            ApplyStraightenSteering(); // Smoothly straighten
        else
            ApplySteering(); // Normal steering

        float speedPercent = rb.velocity.magnitude / (baseMaxSpeed * boostMultiplier);
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

    void ApplyEngineForce(float boost)
    {
        float maxSpeed = baseMaxSpeed * boost;
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);

        if (accelerationInput == 0)
            rb.drag = Mathf.Lerp(rb.drag, 3f, Time.fixedDeltaTime * 3);
        else
            rb.drag = 0;

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        if (velocityVsUp < (-maxSpeed * 0.5f) && accelerationInput < 0)
            return;

        Vector2 engineForce = accelerationFactor * accelerationInput * transform.up * boost;
        rb.AddForce(engineForce, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minTurningSpeedFactor = Mathf.Clamp01(rb.velocity.magnitude / 8);
        float directionMultiplier = (velocityVsUp >= 0) ? 1f : -1f;
        rotationAngle -= steeringInput * turnFactor * minTurningSpeedFactor * directionMultiplier;
        rb.MoveRotation(rotationAngle);
    }

    void ApplyStraightenSteering()
    {
        // Snap toward nearest multiple of 90° but smoothly
        float targetAngle = Mathf.Round(rb.rotation / 90f) * 90f;
        float smoothedAngle = Mathf.LerpAngle(rb.rotation, targetAngle, Time.fixedDeltaTime * straightenSpeed);
        rb.MoveRotation(smoothedAngle);
    }

    void ReduceCarDrift()
    {
        float targetDriftFactor = isDrifting ? 0.95f : 0.4f;
        float transitionSpeed = 5f;
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

    if (collidedObject.tag == "Enemy" || collidedObject.tag == "Pedestrian")
        {
            Rigidbody2D collidedBody = collidedObject.GetComponent<Rigidbody2D>();
            Vector3 diffVector = rb.velocity - collidedBody.velocity;
            
            currentHealth -= collisionDamage * (int)diffVector.magnitude;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            Debug.Log("Dealt the player " + (collisionDamage * (int)diffVector.magnitude) + " damage");
        }
    else
        {
            currentHealth -= collisionDamage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }




            UpdateHealthUI();

    if (currentHealth <= 0)
    {
        isDestroyed = true;
        SoundManager.Instance.StopDrift();
        SoundManager.Instance.StopEngine();

        // Play explosion
        SoundManager.Instance.PlayExplosion();
        VisualEffectsManager.Instance.PlayExplosion(transform.position);

        // Hide car 
        gameObject.SetActive(false);

        // Trigger Game Over UI with delay
        if (GameOverManager.Instance != null)
            GameOverManager.Instance.TriggerGameOver(transform.position);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BoostItem"))
        {
            currentBoost = Mathf.Clamp(currentBoost + 30f, 0, maxBoost);
            UpdateBoostUI();
            Destroy(other.transform.root.gameObject); // Updated line
        }

        if (other.CompareTag("HealthItem"))
        {
            currentHealth = Mathf.Clamp(currentHealth + 25, 0, maxHealth);
            UpdateHealthUI();
            Destroy(other.transform.root.gameObject); // Updated line
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PedestrianWalker pedestrian))
        {
            bool pedestrianKilled = pedestrian.Kill();
            if (pedestrianKilled)
            {
                gameController.IncreasePursuit(15f);
                SoundManager.Instance.PlayPedestrianHitSound();
            }
        }

        HandleDamage(collision.gameObject);
    }
}
