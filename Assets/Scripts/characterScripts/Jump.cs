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
    private Rigidbody2D rb;
    public enum State { Jumping, AirDashing, GroundDashing, Grounded};
    private State currentState;

    public float jumpHeight = 2.71828f;
    private bool doubleJump = false;

    private bool dashAvailable = true;
    public float dashPower = 10f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chrWalk = GetComponentInChildren<Animator>();
        currentState = State.Grounded;
    }

    void Update()
    {
        if (currentState == State.AirDashing || currentState == State.GroundDashing)
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

    private void Jump(float jumpHeight)
    {
        currentState = State.Jumping;
        float jumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
        rb.velocity = new Vector2(0f, jumpForce);
    }
    IEnumerator Dash()
    {
        dashAvailable = false;
        currentState = (currentState == State.Grounded) ? State.GroundDashing : State.AirDashing;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        int originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("NoCollideWallsMonsters");
        int orientation = walkClass.GetCharacterOrientation(chrTransform);
        rb.velocity = new Vector2(orientation * dashPower, 0f);
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        gameObject.layer = originalLayer;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        trailRenderer.emitting = false;
        currentState = (currentState == State.GroundDashing) ? State.Grounded : State.Jumping;
        chrWalk.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCooldown);
        dashAvailable = true;
    }

    void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentState = State.Grounded;
            chrWalk.SetBool("jumping", false);
            chrWalk.SetBool("doubleJump", false);
        }
    }

    void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (currentState == State.Grounded)
            {
                currentState = State.Jumping;
                doubleJump = true;
            }
        }
    }

    public State GetState() {
        return currentState;
    }
}
