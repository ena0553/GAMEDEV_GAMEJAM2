using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float acceleration = 25f;
    public float maxSpeed = 30f;
    public float turnSpeed = 100f;
    public float driftFactor = 0.95f; // lower = more grip, higher = more drift
    public float steeringSpeedThreshold = 15f; /* if the car is going less than
        this speed (either forward or in reverse), it cannot turn at full speed */

    // layers that count as ground for the purpose of checking if the car is grounded
    [SerializeField] private LayerMask groundLayers = ~0; // default is all layers
    [SerializeField] private float groundCheckDistance = 0.15f;

    private Rigidbody rb;
    private BoxCollider carCollider;
    private float moveInput;
    private float turnInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        carCollider = GetComponent<BoxCollider>();

        // lower the center of mass to help prevent flipping
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    void Update()
    {
        // get inputs in Update() to make it more responsive
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        if (!IsGrounded()) // only move if the car is grounded
            return;

        Move();
        Steer();
        ApplyLateralFriction();
    }

    private bool IsGrounded()
    {
        Vector3 origin = carCollider.bounds.center;
        float rayLength = carCollider.bounds.extents.y + groundCheckDistance;

        return Physics.Raycast(
            origin,
            Vector3.down,
            rayLength,
            groundLayers,
            QueryTriggerInteraction.Ignore
        );
    }

    private void Move()
    {
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

        bool canAccelerateForward = moveInput > 0f && forwardSpeed < maxSpeed;
        bool canAccelerateBackward = moveInput < 0f && forwardSpeed > -maxSpeed;

        if (canAccelerateForward || canAccelerateBackward)
        {
            rb.AddForce(
                transform.forward * moveInput * acceleration,
                ForceMode.Acceleration
            );
        }
    }

    private void Steer()
    {
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);
        float speedMagnitude = Mathf.Abs(forwardSpeed);
        // print(speedMagnitude);

        if (speedMagnitude == 0f)
        {
            return;
        }

        float turnCoefficient;
        if (speedMagnitude < steeringSpeedThreshold)
        {
            turnCoefficient = speedMagnitude / steeringSpeedThreshold;
        }
        else
        {
            turnCoefficient = 1f;
        }

        float direction = Mathf.Sign(forwardSpeed);
        float turn = turnInput * turnSpeed * turnCoefficient * direction * Time.fixedDeltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private void ApplyLateralFriction()
    {
        float retainedSidewaysSpeed = Mathf.Clamp01(driftFactor);

        Vector3 sidewaysVelocity =
            transform.right *
            Vector3.Dot(rb.linearVelocity, transform.right);

        float grip = (1f - retainedSidewaysSpeed) / Time.fixedDeltaTime;

        rb.AddForce(
            -sidewaysVelocity * grip,
            ForceMode.Acceleration
        );
    }
}
