using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float fallSpeed = 5f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        
    }

    // Update is called once per frame
    public void Update()
    {
        rb.velocity = new Vector2(0, -fallSpeed);
    }

    public void setFallSpeed(float newFallSpeed)
    {
        fallSpeed = newFallSpeed;
    }

    public float getFallSpeed()
    {
        return fallSpeed;
    }
}
