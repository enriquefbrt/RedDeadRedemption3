using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public Animator chrWalk;
    public float jumpHeight = 2.71828f; // Desired jump height
    private Rigidbody rb; // Reference to the Rigidbody component
    private bool isGrounded = true; // Check if the player is grounded
    private bool doubleJump = false;

    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
        chrWalk = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                chrWalk.SetBool("jumping", true);
                Jump(jumpHeight);
                doubleJump = true;
            }
            else if (doubleJump)
            {
                chrWalk.SetBool("doubleJump", true);
                Jump(jumpHeight * 0.75f);
                doubleJump = false;
            }
        }
    }

    void Jump(float jumpHeight)
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
            chrWalk.SetBool("jumping", false);
            chrWalk.SetBool("doubleJump", false);
        }
    }
}
