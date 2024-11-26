using UnityEngine;

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

    [Header("Player Components")]
    public Rigidbody2D body;
    private Animator animator;

    private bool grounded = true; // Is the player on the ground?
    private bool run = false; // Is the player running?

    private Quaternion lockedRotation;
    private KeyCode rightKey;
    private KeyCode leftKey;
    private KeyCode jumpKey;

    private Vector3 facingRightScale;
    private Vector3 facingLeftScale;

    private void Start()
    {
        // Lock rotation to prevent flipping
        lockedRotation = transform.rotation;

        // Grab references
        animator = GetComponent<Animator>();

        // Set default scale directions
        facingRightScale = transform.localScale;
        facingLeftScale = new Vector3(-facingRightScale.x, facingRightScale.y, facingRightScale.z);

        // Configure controls based on player
        if (player == 1)
        {
            rightKey = KeyCode.RightArrow;
            leftKey = KeyCode.LeftArrow;
            jumpKey = KeyCode.UpArrow;
            // Set default scale directions
            facingLeftScale = transform.localScale;
            facingRightScale = new Vector3(-facingRightScale.x, facingRightScale.y, facingRightScale.z);
        }
        else if (player == 2)
        {
            rightKey = KeyCode.D;
            leftKey = KeyCode.A;
            jumpKey = KeyCode.W;
        }
    }

    private void Update()
    {
        // Lock rotation
        transform.rotation = lockedRotation;

        // Horizontal movement
        float horizontalInput = 0;

        if (Input.GetKey(rightKey))
        {
            horizontalInput = 1; // Move right
            transform.localScale = facingRightScale; // Face right
            run = true;
        }
        else if (Input.GetKey(leftKey))
        {
            horizontalInput = -1; // Move left
            transform.localScale = facingLeftScale; // Face left
            run = true;
        }
        else
        {
            run = false;
            StopRunSound();
        }

        // Apply movement
        if (grounded)
        {
            // Full control on the ground
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            PlayRunSound();
        }
        else
        {
            // Limited control in the air
            body.velocity = new Vector2(horizontalInput * speed * airControlFactor, body.velocity.y);
            StopRunSound();
        }

        // Jumping
        if (Input.GetKeyDown(jumpKey) && grounded)
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
        // else
        // {
        //     Debug.LogWarning("Run AudioSource is not playing or not assigned!");
        // }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
