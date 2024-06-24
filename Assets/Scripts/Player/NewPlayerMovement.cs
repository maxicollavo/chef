using System.Collections;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    private WaitForSeconds walkTime = new WaitForSeconds(5f);

    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slidingSpeed;
    public float crouchSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    public bool isSlowed;

    public float counter;

    private float startYScale;
    [SerializeField]
    GameObject playerCapsule;

    public float slidingDrag;
    public bool isSliding;

    public KeyCode jumpKey;
    public KeyCode sprintKey;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public static NewPlayerMovement Instance;

    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource walkAudio;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        jumping
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + 0.2f, whatIsGround);
        var camForward = cam.transform.forward;
        camForward.y = 0;
        transform.rotation = Quaternion.LookRotation(camForward, Vector3.up);

        if (!GameManager.Instance.onMinigame)
        {
            MyInput();
        }
        
        SpeedControl();
        StateHandler();

        rb.drag = grounded ? groundDrag : 0;

        moveSpeed = isSlowed ? crouchSpeed : moveSpeed;

        if (isSliding)
        {
            rb.drag = slidingDrag;

            if (GameManager.Instance.speedBoost)
            {
                moveSpeed = sprintSpeed / 2;
            }
            else
            {
                moveSpeed = slidingSpeed;
            }

            crouchSpeed = moveSpeed;
        }
        else
        {
            rb.drag = groundDrag;
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.onMinigame)
        {
            MovePlayer();
        }
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded && !isSliding)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // if (Input.GetKeyDown(crouchKey) && !isSliding)
        // {
        //     playerCapsule.transform.localScale = new Vector3(playerCapsule.transform.localScale.x, crouchYScale, playerCapsule.transform.localScale.z);

        //     rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        // }

        // if (Input.GetKeyUp(crouchKey) && !isSliding)
        // {
        //     playerCapsule.transform.localScale = new Vector3(playerCapsule.transform.localScale.x, startYScale, playerCapsule.transform.localScale.z);
        // }
    }

    void StateHandler()
    {
        if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        else
        {
            state = MovementState.jumping;
            moveSpeed = (walkSpeed + sprintSpeed) / 2;
        }
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        rb.useGravity = !OnSlope();
    }

    void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed * 2f;
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    void Jump()
    {
        exitingSlope = true;

        jumpAudio.Play();
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}
