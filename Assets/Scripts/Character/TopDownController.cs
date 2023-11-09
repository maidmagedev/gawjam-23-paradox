using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class TopDownController : MonoBehaviour
{
    public Rigidbody rb;
    private CapsuleCollider bodyCollider;
    float horizontalInput;
    float verticalInput;
    float moveLimiter = 0.7f; 
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private bool MovementDisabled = false;
    public Vector3 moveDir; // used by PlayerAnimations.cs

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bodyCollider = GetComponent<CapsuleCollider>();
        if (MovementDisabled)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
            bodyCollider.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Getting the Player's input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        

        // Some movement scripts use this to calculate velocity values, but this is just used for the animation script.
        moveDir = transform.forward * verticalInput + transform.right * horizontalInput;
        //Debug.DrawRay(transform.position, moveDir * 5, Color.red);

        if (MovementDisabled)
        {
            rb.velocity = Vector3.zero;
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
    private void move()
    {
        // Directly sets the player's velocity based on input
        rb.velocity = new Vector3(horizontalInput * movementSpeed, 0, verticalInput * movementSpeed);
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
    
    public void ToggleMovement()
    {
        MovementDisabled = !MovementDisabled;
        if (MovementDisabled)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
            rb.useGravity = true;
            bodyCollider.enabled = false;
        }
        else
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
            rb.useGravity = false;
            bodyCollider.enabled = true;
            StartCoroutine(delayMoveUp());
        }
    }

    private IEnumerator delayMoveUp()
    {
        yield return new WaitForSeconds(3);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 50, this.transform.position.z);
    }
}
