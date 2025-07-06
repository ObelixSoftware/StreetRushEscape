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

    //Local Variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    float velocityVsUp = 0;

    //Borrowed variables from OG controller
    public int maxHealth = 100;
    public int collisionDamage = 20;
    private int currentHealth;

    public Slider healthBarSlider;

    public float boostStrength = 2.0f;
    private bool isBoosting = false;

    private bool isDrifting = false;

    //Components
    Rigidbody2D rb;
    // Start is called before the first frame update

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
    }

    // Update is called once per frame
    void Update()
    {
        

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
        float boostFactor = 1.0f;
        if (isBoosting)
        {
            boostFactor = 1.5f;
        }


        //Calculate the forward aspect of velocity, used to determine current "speed" relative to the front of the car
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);

        //Slow the car when the player isn't accelerating
        if (accelerationInput == 0)
        {
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            rb.drag = 0;
        }

        //Stops acceleration when the player is already at max speed
        if (velocityVsUp > (maxSpeed * boostFactor) && accelerationInput > 0)
        {
            return;
        }
        if (velocityVsUp < (-maxSpeed * boostFactor) * 0.5f && accelerationInput < 0)
        {
            return;
        }



            Vector2 engineForceVector = accelerationFactor * accelerationInput * transform.up * boostFactor;

        rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minTurningSpeedFactor = (rb.velocity.magnitude / 8);
        minTurningSpeedFactor = Mathf.Clamp01(minTurningSpeedFactor);

        if (velocityVsUp < 0)
        {
            steeringInput = -steeringInput;
        }

        rotationAngle -= steeringInput * turnFactor * minTurningSpeedFactor;

        

        rb.MoveRotation(rotationAngle);
    }

    void ReduceCarDrift()
    {
        if (isDrifting)
        {
            driftFactor = 0.95f;
        }
        else
        {
            driftFactor = 0.4f;
        }
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y; 
    }

    //Functions from original car controller
    void HandleDamage(GameObject collidedObject)
    {
        currentHealth -= collisionDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        Debug.Log($"Car hit! Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("Car destroyed!");
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
    }



}
