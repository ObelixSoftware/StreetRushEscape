using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopDriveHandler : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.4f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;

    public Transform playerTransform;

    //[Header("Health")]
    //public int maxHealth = 100;
    //public int collisionDamage = 20;
    //private int currentHealth;
    //public Slider healthBarSlider;

    //[Header("Explosion")]
    //public GameObject explosionPrefab;
    //public AudioClip explosionSound;
    //private AudioSource audioSource;

    // Movement
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
        rotationAngle = rb.rotation;

        //Find player car
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Car");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null) return;

        //Determine angle to player
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float angleToPlayer = Vector2.SignedAngle(transform.up, directionToPlayer);

        //Turn left or right based on angle to player
        steeringInput = Mathf.Clamp(angleToPlayer / 45f, -1f, 1f);
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();
        ReduceCarDrift();
        ApplySteering();
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);
        if (velocityVsUp > maxSpeed)
            return;

        Vector2 engineForceVector = accelerationFactor * transform.up;
        rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minTurningSpeedFactor = Mathf.Clamp01(rb.velocity.magnitude / 8);

        if (velocityVsUp < 0)
            steeringInput = -steeringInput;

        rotationAngle += steeringInput * turnFactor * minTurningSpeedFactor;
        rb.MoveRotation(rotationAngle);
    }

    void ReduceCarDrift()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
}
