using UnityEngine;

public class AIPlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float speed; // Movement speed
    public Rigidbody2D body;             // Rigidbody2D for physics-based movement
    private Animator animator;           // Animator for controlling animations

    private Transform ball;              // Reference to the current ball
    private Transform player;            // Reference to the player tagged as "Dragon"
    public Transform enemyGoal;          // Reference to the enemy's goal
    public Transform homePosition;       // Defensive position for AI

    private bool grounded = true;        // Is the AI grounded?
    private bool run = false;            // Is the AI running?

    private Quaternion lockedRotation;   // Prevent rotation of the AI object
    private float ballHeightThreshold = 1.5f; // Height above which ball is considered "in the air"

    private void Awake()
    {
        // Lock rotation and get components
        lockedRotation = transform.rotation;
        animator = GetComponent<Animator>();

        // Find the ball and player at the start
        FindBall();
        FindPlayer();
    }

    private void Update()
    {
        // Keep rotation locked
        transform.rotation = lockedRotation;

        // Ensure ball and player references are valid
        if (ball == null)
        {
            FindBall(); // Find the ball again if destroyed
            return;
        }

        if (player == null)
        {
            FindPlayer(); // Find the player if destroyed
            return;
        }

        // AI Movement logic
        Vector3 targetPosition;

        if (IsBallInAir())
        {
            // Ball is in the air; return to home position
            targetPosition = homePosition.position;
        }
        else
        {
            // Ball is on the ground; move toward it
            targetPosition = new Vector3(ball.position.x, transform.position.y, transform.position.z);

            // Push the ball if close enough
            // if (Vector3.Distance(transform.position, ball.position) < 1.5f)
            // {
            //     PushBall();
            // }
        }

        // Move toward the target
        MoveTowards(targetPosition);

        // Set animator parameters
        animator.SetBool("Run", run);
        animator.SetBool("Grounded", grounded);
    }

    private void FindBall()
    {
        // Dynamically find the ball by tag
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
        // Dynamically find the player tagged "Dragon"
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
        // Check if the ball's Y position is above the ground threshold
        return ball.position.y > ballHeightThreshold;
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        // Calculate direction and move
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Flip AI to face the movement direction
        if (direction.x > 0)
            transform.localScale = Vector3.one;
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        run = true;
    }

    private void PushBall()
    {
        // Push the ball towards the enemy goal
        Vector2 directionToGoal = (enemyGoal.position - ball.position).normalized;
        ball.GetComponent<Rigidbody2D>().AddForce(directionToGoal * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground detection
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
