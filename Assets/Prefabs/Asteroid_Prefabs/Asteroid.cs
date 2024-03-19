using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float initialFallSpeed = 5f;
    private float fallSpeedIncreaseRate = 0.1f; // Rate at which the fall speed increases per second
    private float maxFallSpeed = 8f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float currentFallSpeed = initialFallSpeed + (Time.timeSinceLevelLoad * fallSpeedIncreaseRate);
        currentFallSpeed = Mathf.Min(currentFallSpeed, maxFallSpeed); // Cap the fall speed
        rb.velocity = new Vector2(0, -currentFallSpeed);
    }
}
