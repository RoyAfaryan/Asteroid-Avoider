using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public Joystick movementJoystick;
    private float playerSpeed = 3.5f; // Initial player speed
    private float maxSpeed = 7f; // Maximum player speed
    private Rigidbody2D rb;

    [SerializeField]
    private float screenBorder = 10f;
    private Camera camera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        camera = Camera.main;

        // Start coroutine to gradually increase speed
        StartCoroutine(IncreaseSpeed());
    }

    private void FixedUpdate()
    {
        MoveShip();
    }

    private void MoveShip()
    {
        if (movementJoystick.Direction.y != 0)
        {
            rb.velocity = new Vector2(movementJoystick.Direction.x * playerSpeed, movementJoystick.Direction.y * playerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        ScreenBounds();
    }

    private void ScreenBounds()
    {
        Vector2 screenPosition = camera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < screenBorder && rb.velocity.x < 0) ||
            (screenPosition.x > camera.pixelWidth - screenBorder && rb.velocity.x > 0))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if ((screenPosition.y < screenBorder && rb.velocity.y < 0) ||
            (screenPosition.y > camera.pixelHeight - screenBorder && rb.velocity.y > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Wait for 2 seconds

            // Increase speed by 0.1, if it doesn't exceed the maximum speed
            if (playerSpeed < maxSpeed)
            {
                playerSpeed += 0.1f;
            }
        }
    }
}
