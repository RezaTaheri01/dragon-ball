using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBall : MonoBehaviour
{
    public float hitForce = 500f; // Extra force applied on hit

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Get the Rigidbody2D component of the ball
            Rigidbody2D ballRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (ballRigidbody != null)
            {
                // Calculate the direction for the force
                Vector2 direction = (ballRigidbody.position - (Vector2)transform.position).normalized;

                // Apply force to the ball
                ballRigidbody.AddForce(direction * hitForce);
            }
        }
    }
}
