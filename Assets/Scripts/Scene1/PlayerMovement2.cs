using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField] private float speed;

    public Rigidbody2D body;
    private Animator animator;
    private bool grounded = true;
    private bool run = false;

    private Quaternion lockedRotation;

    private void Awake()
    {
        // Grab references from rigidbody and animator from object
        lockedRotation = transform.rotation;
        // body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // change run value
    }

    private void Update()
    {
        transform.rotation = lockedRotation;
        // float horizontalInput = Input.GetAxis("Horizontal");
        // body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            // flip player
            transform.localScale = Vector3.one;
            run = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.right * -speed * Time.deltaTime;
            // flip player
            transform.localScale = new Vector3(-1, 1, 1);
            run = true;
        }
        else
        {
            run = false;
        }

        if (Input.GetKey(KeyCode.W) && grounded)
            Jump();


        // Set animator parameter
        animator.SetBool("Run", run);
        // update animator grounded
        animator.SetBool("Grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        animator.SetTrigger("Jump");
        grounded = false;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
        else if (collision.gameObject.tag == "Ball")
            grounded = true;
    }
}