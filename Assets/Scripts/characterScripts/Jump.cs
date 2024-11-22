using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public float jumpHeight = 50f; // Desired jump height
    public float gravityMultiplier = 3f; // Multiplier to speed up upward and downward motion
    private Rigidbody rb; // Reference to the Rigidbody component
    private bool isGrounded = true; // Check if the player is grounded
    private float originalGravity;

    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();

        // Store the original gravity value
        originalGravity = Physics.gravity.y;

        // Increase gravity for faster jumps
        Physics.gravity = new Vector3(0, originalGravity * gravityMultiplier, 0);
    }

    void Update()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        // Calculate the required jump force to reach the desired height
        float jumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y / gravityMultiplier) * jumpHeight);

        // Apply the jump force directly
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

        isGrounded = false; // Player is no longer grounded after the jump
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player touches the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
