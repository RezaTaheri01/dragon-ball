using UnityEngine;

public class AIPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;               // Movement speed
    [SerializeField] private float airControlFactor = 0.5f; // Factor for movement when in air
    public Rigidbody2D body;                           // Rigidbody2D for physics-based movement
    private Animator animator;                         // Animator for controlling animations

    private Transform ball;                            // Reference to the ball
    private Transform player;                          // Reference to the player tagged as "Dragon"
    public Transform enemyGoal;                        // Reference to the enemy's goal
    public Transform homePosition;                     // Defensive home position for AI

    private bool grounded = true;                      // Is the AI grounded?
    private bool run = false;                          // Is the AI running?

    private Quaternion lockedRotation;                 // Lock the AI's rotation
    private float ballHeightThreshold = 1.5f;          // Height above which the ball is considered "in the air"

    private float flipTolerance = 0.1f;                // Tolerance to prevent rapid flipping
    private float hitRange = 1.5f;                     // Distance within which the AI will hit the ball
    // private float hitForce = 500f;                     // Force applied to the ball on hit

    private void Awake()
    {
        lockedRotation = transform.rotation;
        animator = GetComponent<Animator>();

        FindBall();
        FindPlayer();
    }

    private void Update()
    {
        transform.rotation = lockedRotation;

        if (ball == null)
        {
            FindBall();
            return;
        }

        if (player == null)
        {
            FindPlayer();
            return;
        }

        if (IsCloseToBall())
        {
            HitBall();
            return;
        }

        Vector3 targetPosition;

        if (IsBallInAir())
        {
            targetPosition = new Vector3(ball.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            targetPosition = ball.position;
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

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Dragon");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player tagged 'Dragon' not found! Ensure the correct player tag is assigned.");
        }
    }

    private bool IsBallInAir()
    {
        return ball.position.y > ballHeightThreshold;
    }

    private bool IsCloseToBall()
    {
        return Vector3.Distance(transform.position, ball.position) <= hitRange;
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (IsBallInAir())
        {
            transform.position += direction * speed * airControlFactor * Time.deltaTime;
        }
        else
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

    private void HitBall()
    {
        if (ball == null) return;

        // Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
        // if (ballRigidbody != null)
        // {
        //     Vector2 directionToGoal = (enemyGoal.position - ball.position).normalized;
        //     ballRigidbody.AddForce(directionToGoal * hitForce, ForceMode2D.Impulse);
        //     Debug.Log("Ball hit toward the enemy goal!");
        // }
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
