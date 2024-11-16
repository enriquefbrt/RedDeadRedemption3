using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public float jumpForce = 5f; // Adjust the jump force as needed
    private Rigidbody rb;       // Reference to the Rigidbody component
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
        // Apply a vertical force for the jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
