using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    public int player = 1; // Player identifier (1 or 2)
    public AudioSource jumpAudioSource;
    public AudioSource runAudioSource;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f; // Horizontal movement speed
    [SerializeField] private float jumpForce = 10f; // Vertical jump force
    [SerializeField] private float airControlFactor = 0.5f; // Control factor for air movement
    [SerializeField] private float jumpThreshold = 0.7f; // Threshold for jumping from joystick input

    [Header("Player Components")]
    public Rigidbody2D body;
    private Animator animator;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveActionReference; // Reference for joystick movement

    private Vector2 movementInput; // Input vector for movement
    private bool grounded = true; // Is the player on the ground?
    private bool run = false; // Is the player running?

    private Quaternion lockedRotation;

    private Vector3 facingRightScale;
    private Vector3 facingLeftScale;

    private void Awake()
    {
        // Grab references
        animator = GetComponent<Animator>();

        // Lock rotation to prevent flipping
        lockedRotation = transform.rotation;

        // Set default scale directions
        facingRightScale = transform.localScale;
        facingLeftScale = new Vector3(-facingRightScale.x, facingRightScale.y, facingRightScale.z);
    }

    private void OnEnable()
    {
        // Enable input actions
        moveActionReference.action.Enable();
    }

    private void OnDisable()
    {
        // Disable input actions
        moveActionReference.action.Disable();
    }

    private void Update()
    {
        // Lock rotation
        transform.rotation = lockedRotation;

        // Get movement input
        movementInput = moveActionReference.action.ReadValue<Vector2>();

        // Determine running state
        run = Mathf.Abs(movementInput.x) > 0.1f;

        if (run)
        {
            PlayRunSound();
        }
        else
        {
            StopRunSound();
        }

        // Flip player base on direction
        if (player == 1){
            if (movementInput.x < 0)
            {
                transform.localScale = facingRightScale;
            }
            else if (movementInput.x > 0)
            {
                transform.localScale = facingLeftScale;
            }
        }
        else if (movementInput.x > 0)
        {
            transform.localScale = facingRightScale;
        }
        else if (movementInput.x < 0)
        {
            transform.localScale = facingLeftScale;
        }

        // Apply horizontal movement
        if (grounded)
        {
            body.velocity = new Vector2(movementInput.x * speed, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(movementInput.x * speed * airControlFactor, body.velocity.y);
        }

        // Check for jump input from joystick
        if (movementInput.y > jumpThreshold && grounded)
        {
            Jump();
            PlayJumpSound();
        }

        // Update animator parameters
        animator.SetBool("Run", run);
        animator.SetBool("Grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce); // Apply upward force
        animator.SetTrigger("Jump");
        grounded = false; // Not grounded anymore
    }

    private void PlayJumpSound()
    {
        if (jumpAudioSource != null && jumpAudioSource.clip != null)
        {
            jumpAudioSource.PlayOneShot(jumpAudioSource.clip);
        }
        else
        {
            Debug.LogWarning("Jump AudioSource or clip not assigned!");
        }
    }

    private void PlayRunSound()
    {
        if (runAudioSource != null && runAudioSource.clip != null)
        {
            if (!runAudioSource.isPlaying)
            {
                // Set the AudioSource to loop
                runAudioSource.loop = true;
                runAudioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("Run AudioSource or clip not assigned!");
        }
    }

    private void StopRunSound()
    {
        if (runAudioSource != null && runAudioSource.isPlaying)
        {
            // Stop the AudioSource
            runAudioSource.Stop();

            // Disable looping to avoid unexpected behavior for other sounds
            runAudioSource.loop = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
