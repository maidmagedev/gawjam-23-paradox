using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlatformingMovementComponent : MonoBehaviour
{
    // Components
    public Rigidbody2D rb;
    PolygonCollider2D BodyCollider;
    BoxCollider2D FeetCollider;

    // Serialized Fields
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 11f;
    [SerializeField] float heldjumpSpeed = 5f;
    [SerializeField] public bool MovementDisabled = false;

    // floats
    private float mayJump = 0.1f;  // coyote time: make this longer for more coyote time
    private float timer;
    private float horizontalInput;

    // booleans
    private bool isJumping = false;
    public bool isGrounded = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BodyCollider = GetComponent<PolygonCollider2D>();
        FeetCollider = GetComponent<BoxCollider2D>();

        // Setting up the Rigidbody2D
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sharedMaterial.friction = 0;

        // Setting the gravity  
        Physics2D.gravity = new Vector2(0, -38);

    }

    // Update is called once per frame
    void Update()
    {
        if (MovementDisabled)
        {
            // may be unnecessary...
            rb.velocity = Vector2.zero;
            return;
        }
        Jump();
    }

    private void FixedUpdate()
    {
        if (MovementDisabled)
        {
            // may be unnecessary...
            rb.velocity = Vector2.zero;
            return;
        }
        Run();
    }

    // Movement Functions
    private void Run()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * runSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        mayJump -= Time.deltaTime;

        if (FeetCollider.IsTouchingLayers(LayerMask.GetMask("SideScroller Ground")))
        {
            isGrounded = true;
            mayJump = 0.1f; // coyote time: see above comment
        } else {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && (mayJump > 0f))
        {
            //print("jumping");
            timer = Time.time;
            isJumping = true;
            rb.velocity = new Vector2(0f, jumpSpeed);
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (Time.time - timer > 0.15) // make 0.15 bigger for longer time needed to hold down jump
            {
                timer = float.PositiveInfinity;
                rb.velocity += new Vector2(0f, heldjumpSpeed);
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    // Non-movement functions 
    private void FlipSprite()
    {
        // adjust the -1 and 1 as needed depending on the sprite scale being used
        if (horizontalInput < 0)
        {
            gameObject.transform.localScale = new Vector2(-1, gameObject.transform.localScale.y);
        }
        else if (horizontalInput > 0)
        {
            gameObject.transform.localScale = new Vector2(1, gameObject.transform.localScale.y);
        }
    }

    public void ToggleMovement()
    {
        MovementDisabled = !MovementDisabled;

        // This was sketchy, and removed in favor of handling this in PerspectiveManager.cs instead.
        // // jump to the bottom after top down
        // if (!MovementDisabled) {
        //     StartCoroutine(ForcetoBottom());
        // }
    }

    IEnumerator ForcetoBottom() {
        rb.gravityScale = 100;
        yield return new WaitForSeconds(0.1f);
        rb.gravityScale = 1;
    }

    public bool GetIsJumping() {
        return isJumping;
    }

    
}
