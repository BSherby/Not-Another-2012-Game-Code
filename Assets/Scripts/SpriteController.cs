using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    // Adjust the movement speed
    public float moveSpeed = 5.0f;
    // Adjust the rotation speed
    public float rotationSpeed = 10.0f;
    // Adjust the jump force
    public float jumpForce = 5.0f;
    // A point representing where to check for the ground
    public Transform[] groundChecks;
    // The radius of the ground check
    public float groundCheckRadius = 0.2f;
    // Layer used to identify the ground
    public LayerMask groundLayer;
    //Defining boundary variables
    public float leftBoundary = -7f;
    public float rightBoundary = 9f;

    private bool isGrounded;
    private bool IsGrounded()
    {
        foreach (Transform checkPoint in groundChecks)
        {
            if (Physics2D.OverlapCircle(checkPoint.position, groundCheckRadius, groundLayer))
            {
                return true; //Return true as soon as one of the checks finds ground
            }
        }
        return false; //Return false if none of the checks find ground
    }
    private Animator animator; // Reference to the Animator component
    private float jumpBufferTime = 0.01f; //100ms to buffer jump
    private float lastJumpInputTime = -0.1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = IsGrounded();

        // Ground check
        isGrounded = false;
        foreach (Transform checkPoint in groundChecks)
        {
            if (Physics2D.OverlapCircle(checkPoint.position, groundCheckRadius, groundLayer))
            {
                isGrounded = true;
                break; //If any ground check is true, set isGrounded to true and exit loop
            }
        }

        // Get player input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement vector based on input
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Correctly use the horizontal input value
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Jump input and check if grounded
        if (Input.GetButtonDown("Jump") && isGrounded && (GetComponent<Rigidbody2D>().velocity.y == 0))
        {
            // Apply an initial jump force
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        else if (Input.GetButtonUp("Jump") && GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            // Reduce the y velocity when the jump button is released, making the jump height variable
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y * 0.5f);
        }

        if (Input.GetButton("Jump"))
        {
            lastJumpInputTime = Time.time;
        }
        //When grounded and jump input was recently received
        if (isGrounded && Time.time - lastJumpInputTime <= jumpBufferTime)
        {
            Jump();
            lastJumpInputTime = -0.1f; //Reset Buffer
        }

        // Check if movement vector is not zero
        if (movement != Vector2.zero)
        {
            // Calculate angle towards movement direction
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

            // Set rotation towards the movement direction
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90)); // Subtracting 90 degrees to align the sprite facing up

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Apply movement
        transform.position += (Vector3)movement.normalized * moveSpeed * Time.deltaTime;

        //Apply movement
        transform.position += (Vector3)movement.normalized * moveSpeed * Time.deltaTime;

        //Clamping the sprite's position within the specified boundaries
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBoundary, rightBoundary), transform.position.y, transform.position.z);
    }

    void OnDrawGizmosSelected()
    {
        // Check if the groundChecks array is not null and has elements
        if (groundChecks != null && groundChecks.Length > 0)
        {
            //Set the Gizmo color to red
            Gizmos.color = Color.red;

            //Iterate through each Transform in the groundChecks array
            foreach (Transform checkPoint in groundChecks)
            {
                //For each, draw a wire sphere at the checkPoint's position with the specified radius
                Gizmos.DrawWireSphere(checkPoint.position, groundCheckRadius);
            }
        }
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
    }

    void Jump()
    {
        //Apply jump force
    }

}
