using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // Required for TextMesh Pro

public class AIPlayerMovementBoss : MonoBehaviour
{
    [SerializeField] private float speed = 5f;               // Movement speed
    [SerializeField] private float airControlFactor = 0.5f;  // Factor for movement when in air
    [SerializeField] private float controlRange = 3f;        // Maximum distance to control the ball
    [SerializeField] private float ballOffset = 1f;          // Distance to position the ball on the left side
    private Animator animator;                               // Animator for controlling animations

    private Transform ball;                                  // Reference to the ball
    public Transform homePosition;                           // Defensive home position for AI
    public Transform enemyHomePosition;                     // Enemy home position

    private bool grounded = true;                           // Is the AI grounded?
    private bool run = false;                               // Is the AI running?

    private Quaternion lockedRotation;                      // Lock the AI's rotation
    private float ballHeightThreshold = 5f;                 // Height above which the ball is considered "in the air"
    private float flipTolerance = 0.1f;                     // Tolerance to prevent rapid flipping
    
    private void Awake()
    {
        
        lockedRotation = transform.rotation;
        animator = GetComponent<Animator>();
        FindBall();

        // Start the coroutine to hide it after a delay
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

        targetPosition = ball.position; // Move toward the ball

        MoveTowards(targetPosition);

        if (IsBallInRange())
        {
            // if (IsBallAbove())
            // {
            //     ShootBallToEnemyHome();
            // }
            Debug.Log("ball is on range");
            if (IsOnLeftSideOfBall())
            {   Debug.Log("ball is on left side");
                MoveBallToLeftOfPlayer();
            }
        }

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
        float height = GetComponent<Collider2D>().bounds.size.y;
        float width = GetComponent<Collider2D>().bounds.size.x;

        return transform.position.x < ball.position.x && transform.position.y + height > ball.position.y;
    }

    private bool IsBallInRange()
    {
        // Check if the ball is within the control range
        float distanceToBall = Vector2.Distance(transform.position, ball.position);
        return distanceToBall <= controlRange;
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
                transform.localScale = new Vector3(15, 15, 15);
            else if (directionX < 0)
                transform.localScale = new Vector3(-15, 15, 15);
        }
    }

    private void MoveBallToLeftOfPlayer()
    {
        if (ball != null)
        {
            // Position the ball on the left side of the AI player
            Vector3 newPosition = transform.position - new Vector3(ballOffset, 0, 0);
            ball.position = newPosition;
            Debug.Log("Ball moved to the left side of the player at position: " + newPosition);
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
