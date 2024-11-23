using UnityEngine;

public class AIPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;               // Movement speed
    [SerializeField] private float airControlFactor = 0.5f; // Factor for movement when in air
    private Animator animator;                         // Animator for controlling animations

    private Transform ball;                            // Reference to the ball
    public Transform homePosition;                     // Defensive home position for AI

    private bool grounded = true;                      // Is the AI grounded?
    private bool run = false;                          // Is the AI running?

    private Quaternion lockedRotation;                 // Lock the AI's rotation
    private float ballHeightThreshold = 1.5f;          // Height above which the ball is considered "in the air"

    private float flipTolerance = 0.1f;                // Tolerance to prevent rapid flipping
    private float positioningRange = 1.0f;             // Distance from the left side of the ball AI should aim for

    private void Awake()
    {
        lockedRotation = transform.rotation;
        animator = GetComponent<Animator>();
        FindBall();
    }

    private void Update()
    {
        transform.rotation = lockedRotation;

        if (ball == null)
        {
            FindBall();
            return;
        }

        Vector3 targetPosition;

        // Always try to position AI to the left of the ball
        if (IsOnLeftSideOfBall())
        {
            targetPosition = ball.position; // Move toward the ball
        }
        else
        {
            // Move to a point slightly left of the ball
            targetPosition = new Vector3(ball.position.x - positioningRange, transform.position.y, transform.position.z);
        }

        MoveTowards(targetPosition);

        animator.SetBool("Run", run);
        animator.SetBool("Grounded", grounded);
    }

    private void FindBall()
    {
        GameObject ballObject = GameObject.FindGameObjectWithTag("Ball");
        if (ballObject != null)
        {
            ball = ballObject.transform;
        }
        else
        {
            Debug.LogWarning("Ball not found in the scene! Ensure it has the correct tag.");
        }
    }

    private bool IsOnLeftSideOfBall()
    {
        // Check if the AI is to the left of the ball
        return transform.position.x < ball.position.x;
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (ball.position.y > ballHeightThreshold) // Ball in air
        {
            transform.position += direction * speed * airControlFactor * Time.deltaTime;
        }
        else // Ball on ground
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        HandleFlip(direction.x);

        run = true;
    }

    private void HandleFlip(float directionX)
    {
        if (Mathf.Abs(directionX) > flipTolerance)
        {
            if (directionX > 0)
                transform.localScale = Vector3.one;
            else if (directionX < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = false;
    }
}
