using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public float jumpHeight = 2.71828f; // Desired jump height
    private Rigidbody rb; // Reference to the Rigidbody component
    private bool isGrounded = true; // Check if the player is grounded

    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
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
        float jumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);

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
