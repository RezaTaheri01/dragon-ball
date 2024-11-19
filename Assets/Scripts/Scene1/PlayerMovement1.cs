using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    [SerializeField] private float speed;

    public Rigidbody2D body;
    private Animator animator;
    private bool grounded = true;

    private bool run = false;

    private Quaternion lockedRotation;

    private void Awake()
    {
        Debug.Log("Start");
        // Grab references from rigidbody and animator from object
        lockedRotation = transform.rotation;
        // body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // change run value
    }

    private void Update()
    {
        transform.rotation = lockedRotation;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            // flip player
            transform.localScale = new Vector3(5, 5, 5);
            run = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.right * -speed * Time.deltaTime;
            // flip player
            transform.localScale = new Vector3(-5, 5, 5);
            run = true;
        }
        else
        {
            run = false;
        }

        if (Input.GetKey(KeyCode.UpArrow) && grounded)
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