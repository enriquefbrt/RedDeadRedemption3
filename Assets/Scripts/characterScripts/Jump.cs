using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JumpScript : MonoBehaviour
{
    public Animator chrWalk;
    public Transform chrTransform;
    public TrailRenderer trailRenderer;
    public Walk walkClass;
    private Rigidbody rb; // Reference to the Rigidbody component
    private enum State { Jumping, AirDashing, GroundDashing, Grounded};
    private State currentState;

    public float jumpHeight = 2.71828f; // Desired jump height
    private bool doubleJump = false;

    private bool dashAvailable = true;
    public float dashPower = 10f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
        chrWalk = GetComponentInChildren<Animator>();
        currentState = State.Grounded;
    }

    void Update()
    {
        if (currentState == State.AirDashing | currentState == State.GroundDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.C) && dashAvailable)
        {
            chrWalk.SetBool("isDashing", true);
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentState == State.Grounded)
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

        currentState = State.Jumping; // Player is no longer grounded after the jump
    }
    IEnumerator Dash()
    {
        dashAvailable = false;
        currentState = (currentState == State.Grounded) ? State.GroundDashing : State.AirDashing;
        int orientation = walkClass.GetCharacterOrientation(chrTransform);
        rb.velocity = new Vector3(orientation * dashPower, 0f, 0f);
        rb.useGravity = false;
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashTime);

        rb.useGravity = true;
        trailRenderer.emitting = false;
        currentState = (currentState == State.GroundDashing) ? State.Grounded : State.Jumping;
        chrWalk.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCooldown);
        dashAvailable = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player touches the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentState = State.Grounded;
            chrWalk.SetBool("jumping", false);
            chrWalk.SetBool("doubleJump", false);
        }
    }
}
