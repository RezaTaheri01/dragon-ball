using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMile : MonoBehaviour
{
    public AudioSource hitMileAudioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Play hit sound
            if (hitMileAudioSource != null && hitMileAudioSource.clip != null)
            {
                if (!hitMileAudioSource.isPlaying)
                {
                    hitMileAudioSource.PlayOneShot(hitMileAudioSource.clip);
                }
            }
            else
            {
                Debug.LogWarning("Hit Mile AudioSource or clip not assigned!");
            }
        }
    }
}
