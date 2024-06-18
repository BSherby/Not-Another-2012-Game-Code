using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 10.0f;
    public float jumpForce = 5.0f;
    public Transform[] groundChecks;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float leftBoundary = -8.2f;
    public float rightBoundary = 10.6f;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool IsGrounded()
    {
        foreach (Transform checkPoint in groundChecks)
        {
            if (Physics2D.OverlapCircle(checkPoint.position, groundCheckRadius, groundLayer))
            {
                return true;
            }
        }
        return false;
    }
    private Animator animator;
    private float jumpBufferTime = 0f;
    private float lastJumpInputTime = 0f;
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        isGrounded = IsGrounded();

        // Ground check
        isGrounded = false;
        foreach (Transform checkPoint in groundChecks)
        {
            if (Physics2D.OverlapCircle(checkPoint.position, groundCheckRadius, groundLayer))
            {
                isGrounded = true;
                break;
            }
        }

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

        if (movement != Vector2.zero)
        {
            // Calculate angle towards movement direction
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

            // Set rotation towards the movement direction
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90)); // Subtracting 90 degrees to align the sprite facing up

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        transform.position += (Vector3)movement.normalized * moveSpeed * Time.deltaTime;

        transform.position += (Vector3)movement.normalized * moveSpeed * Time.deltaTime;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBoundary, rightBoundary), transform.position.y, transform.position.z);
    }

    void OnDrawGizmosSelected()
    {
        if (groundChecks != null && groundChecks.Length > 0)
        {
            Gizmos.color = Color.red;

            foreach (Transform checkPoint in groundChecks)
            {
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
       
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }
    public void AllowMovement()
    {
        canMove = true;
    }
}
