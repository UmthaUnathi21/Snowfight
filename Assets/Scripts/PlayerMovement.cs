using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public string playerColor; // "Blue" or "Green"
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animtr;

    public Transform groundCheck;  // Check if player is on the ground
    public float groundCheckRadius = 0.2f;  // Radius ground check
    public LayerMask groundLayer;  // Check against ground

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animtr = GetComponent<Animator>();
    }

    private void Update()
    {
        // Perform ground check using a mini-circle/radius check below player
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if(rb.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.linearVelocity.x > 0) 
        { 
            transform.localScale = new Vector3(1, 1, 1); 
        }

        animtr.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animtr.SetBool("Ground", isGrounded);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        Vector2 movement = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = movement;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false; // Immediately set to false to prevent mid-air jumps
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true; // Set grounded to true only when touching the ground
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false; // Set grounded to false when leaving the ground
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);  // Draws a mini-circle around the ground check
    }

}
