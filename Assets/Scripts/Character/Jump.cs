using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JumpScript : MonoBehaviour
{
    [SerializeField] private Animator chrWalk;
    [SerializeField] private Transform chrTransform;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Walk walkClass;
    [SerializeField] private GameObject childCollision;
    private Rigidbody2D rb;
    public enum State { Jumping, AirDashing, GroundDashing, Grounded};
    private State currentState;

    [SerializeField] private float jumpHeight = 2.71828f;
    private bool doubleJump = false;

    private bool dashAvailable = true;
    [SerializeField] private float dashPower = 15f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

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
        int originalChildLayer = childCollision.layer;
        gameObject.layer = LayerMask.NameToLayer("NoCollideWallsMonsters");
        childCollision.layer = LayerMask.NameToLayer("NoCollideWallsMonsters");
        int orientation = walkClass.GetCharacterOrientation(chrTransform);
        rb.velocity = new Vector2(orientation * dashPower, 0f);
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        gameObject.layer = originalLayer;
        childCollision.layer = originalChildLayer;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        trailRenderer.emitting = false;
        currentState = (currentState == State.GroundDashing) ? State.Grounded : State.Jumping;
        chrWalk.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCooldown);
        dashAvailable = true;
    }

    void OnCollisionStay2D(UnityEngine.Collision2D collision)
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
