using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TopDownMovementComponent : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontalInput;
    float verticalInput;
    float moveLimiter = 0.7f;
    [SerializeField] private float movementSpeed = 8f;
    private bool MovementDisabled = false;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        // initializing the rigidbody
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting the Player's input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Player Death
        if (MovementDisabled)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (horizontalInput != 0 && verticalInput != 0)
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontalInput *= moveLimiter;
            verticalInput *= moveLimiter;
        }
        move();
    }

    private void FixedUpdate()
    {
        //moveAddForce();
    }

    public void DisableMovement()
    {
        rb.velocity = Vector2.zero;
        MovementDisabled = true;
    }

    private void move()
    {
        // Directly sets the player's velocity based on input
        rb.velocity = new Vector3(horizontalInput * movementSpeed, 0, verticalInput * movementSpeed);
    }

    private void moveAddForce()
    {
        // movement with this method feels a little worse, but it avoids setting velocity directly
        rb.AddForce(new Vector2(horizontalInput * 80, verticalInput * 80));
    }


    // Rotates the sprite based off mouse position--currently flips left or right only
    private void RotateSprite()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
        if (Mathf.Abs(angle) > 150)
        {
            // facing left
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x, -0.3f);
        }
        else if (Mathf.Abs(angle) < 40)
        {
            // facing right
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x, 0.3f);
        }

    }

    // Flips the sprite on the y axis based off the player's movement direction
    private void FlipSprite()  
    {
        if (rb.velocity.x < 0) // made this velocity based instead
        {
            gameObject.transform.localScale = new Vector2(-0.3f, gameObject.transform.localScale.y);
        }
        else if (rb.velocity.x > 0)
        {
            gameObject.transform.localScale = new Vector2(0.3f, gameObject.transform.localScale.y);
        }

    }
}
