using UnityEngine;

public class Shield : MonoBehaviour
{
    public float deflectForce = 50f; // Force to apply to asteroids upon collision

    // collision handler
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an asteroid
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Get the Rigidbody2D component of the asteroid
            Rigidbody2D asteroidRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            // Disable the AsteroidMovement script on collision
            Asteroid asteroidMovement = collision.gameObject.GetComponent<Asteroid>();
            if (asteroidMovement != null)
            {
                asteroidMovement.enabled = false;
            }

            // Disable the Collider component of the asteroid
            Collider2D asteroidCollider = collision.gameObject.GetComponent<Collider2D>();
            if (asteroidCollider != null)
            {
                asteroidCollider.enabled = false;
            }

            if (asteroidRigidbody != null)
            {
                
                // Calculate the direction away from the shield
                Vector2 deflectDirection = (collision.transform.position - transform.position).normalized;

                // Apply the force to deflect the asteroid
                asteroidRigidbody.AddForce(transform.up * deflectForce, ForceMode2D.Impulse);
            }
        }
    }



}
