using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
[SerializeField] private float speed; // Horizontal movement speed
    [SerializeField] private float jumpForce; // Vertical jump force
    [SerializeField] private float airControlFactor = 0.5f; // Control factor for air movement

    public Rigidbody2D body;
    private Animator animator;
    private bool grounded = true;
    private bool run = false;
    private Quaternion lockedRotation;

    private void Awake()
    {
        // Grab references from Rigidbody and Animator
        lockedRotation = transform.rotation;
        animator = GetComponent<Animator>(); // change run value
    }

    private void Update()
    {
        transform.rotation = lockedRotation; // Lock rotation to prevent flipping

        // Horizontal movement
        float horizontalInput = 0;

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1; // Move right
            transform.localScale = Vector3.one;; // Flip sprite to face right
            run = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1; // Move left
            transform.localScale = new Vector3(-1, 1, 1); // Flip sprite to face left
            run = true;
        }
        else
        {
            run = false;
        }

        // Apply movement based on grounded state
        if (grounded)
        {
            // Full control on the ground
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }
        else
        {
            // Limited air control
            body.velocity = new Vector2(horizontalInput * speed * airControlFactor, body.velocity.y);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            Jump();
        }

        // Update animator parameters
        animator.SetBool("Run", run);
        animator.SetBool("Grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce); // Apply upward force for jump
        animator.SetTrigger("Jump");
        grounded = false; // Player is no longer grounded
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
        // else if (collision.gameObject.tag == "Ball")
        //     grounded = true;
    }
}