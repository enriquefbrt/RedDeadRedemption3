using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public Animator chrWalk;
    public float jumpHeight = 2.71828f; // Desired jump height
    private Rigidbody rb; // Reference to the Rigidbody component
    private bool isGrounded = true; // Check if the player is grounded

    private float _timeKeyPressed = 0f; // Tiempo acumulado de la tecla presionada
    private bool _isKeyBeingPressed = false; // Estado de la tecla

    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
        chrWalk = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            chrWalk.SetBool("spacebar", true);
            _isKeyBeingPressed = true;
            _timeKeyPressed = 0f;
        }

        if (Input.GetKey(KeyCode.Space) && _isKeyBeingPressed)
        {
            _timeKeyPressed += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            chrWalk.SetBool("spacebar", false);
            chrWalk.SetBool("jumping", true);
            Jump();
            _isKeyBeingPressed = false;
        }
    }

    void Jump()
    {
        // Calculate the required jump force to reach the desired height
        float jumpForce;
        if (_timeKeyPressed < 0.75f) { jumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight); }
        else { jumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * 1.75f * jumpHeight); }

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
        }
    }
}
