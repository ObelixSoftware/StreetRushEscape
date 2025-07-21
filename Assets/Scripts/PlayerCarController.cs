using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCarController : MonoBehaviour
{
    public float acceleration = 5f;
    public float maxForwardSpeed = 6f;
    public float maxReverseSpeed = 6f;
    public float steeringSpeed = 200f;
    public float bounceBackDistance = 1f;
    public float bounceBackSpeed = 5f;

    public float driftDrag = 0.05f;
    public float normalDrag = 1f;
    public float driftSteeringMultiplier = 2.5f;
    public float driftSpeedBoost = 3f;

    public float boostMultiplier = 2f;
    public float boostDuration = 2f;
    private bool isBoosting = false;
    private float boostEndTime = 0f;
    [Header("Health")]
    public int maxHealth = 100;

    public int collisionDamage = 20;
    private int currentHealth;

    public Slider healthBarSlider;

    private float currentSpeed = 0f;
    private float steerInput = 0f;
    private Rigidbody2D rb;
    private bool isBouncing = false;
    private Vector2 bounceDirection;

    [Header("Drift Smoke Spawn Point")]
    public Transform driftSmokeSpawnPoint;
    private bool isDrifting = false;
    private bool isDestroyed = false;
    private bool isFrozen = false;

    private AudioSource audioSource;

    // Drift smoke timing
    private float driftSmokeTimer = 0f;
    public float driftSmokeInterval = 0.05f; // How often to spawn smoke

    private float maxSpeed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.drag = normalDrag;

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
        if (isBouncing || isFrozen) return;

        isDrifting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetKeyDown(KeyCode.X) && !isBoosting)
            StartBoost();

        bool forward = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool reverse = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (forward)
        {
            currentSpeed += acceleration * Time.deltaTime;
            maxSpeed = isDrifting ? maxForwardSpeed + driftSpeedBoost : maxForwardSpeed;
            maxSpeed = isBoosting ? maxSpeed * boostMultiplier : maxSpeed;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
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

        if (isBoosting && Time.time >= boostEndTime)
            StopBoost();
    }

    void FixedUpdate()
    {
        if (isBouncing || isFrozen)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.drag = isDrifting ? driftDrag : normalDrag;

        float speedPercent = rb.velocity.magnitude / maxSpeed;
        SoundManager.Instance.UpdateEngineSound(speedPercent);

        if (Mathf.Abs(currentSpeed) > 0.1f && steerInput != 0f)
        {
            float direction = currentSpeed >= 0 ? 1f : -1f;
            float steerMultiplier = isDrifting ? driftSteeringMultiplier : 1f;
            rb.MoveRotation(rb.rotation + steerInput * steeringSpeed * Time.fixedDeltaTime * direction * steerMultiplier);
        }
        else
        {
            float snappedRotation = Mathf.Round(rb.rotation / 90f) * 90f;
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, snappedRotation, Time.fixedDeltaTime * 5f));
        }

        diftingEffect();

        rb.velocity = transform.up * currentSpeed;
    }

    void diftingEffect() {
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
            driftSmokeTimer = 0f;
        }  
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBouncing) return;

        if (collision.contacts.Length > 0)
        {
            GameObject other = collision.gameObject;

            bounceDirection = collision.contacts[0].normal;
            currentSpeed = 0f;
            isBouncing = true;
            Invoke(nameof(StopBounce), 0.2f);
            //HandleDamage(other);
        }
        if (collision.gameObject.TryGetComponent(out PedestrianWalker pedestrian))
        {
           bool pedestrianKilled = pedestrian.Kill();

            //if (pedestrianKilled)
            //    gameController.IncreasePursuit(15f);
        }
    }

    IEnumerator FreezeCar(float duration)
    {
        isFrozen = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(duration);
        isFrozen = false;
    }

    void HandleDamage(GameObject collidedObject)
    {
        if (isDestroyed) return;

        currentHealth -= collisionDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        Debug.Log($"[PlayerController] Car hit! Current health: {currentHealth}");

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

    void StopBounce()
    {
        isBouncing = false;
        rb.velocity = Vector2.zero;
    }

    private void StartBoost()
    {
        isBoosting = true;
        boostEndTime = Time.time + boostDuration;
        Debug.Log("Boost activated!");
    }

    private void StopBoost()
    {
        isBoosting = false;
        Debug.Log("Boost ended.");
    }
}
