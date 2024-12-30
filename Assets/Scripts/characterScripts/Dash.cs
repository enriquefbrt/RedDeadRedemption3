using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DashScript : MonoBehaviour
{
    public Animator chrWalk;
    public Transform chrTransform;
    private Rigidbody rb; // Reference to the Rigidbody component
    public TrailRenderer trailRenderer;

    private bool isDashing = false;
    private bool dashAvailable = true;
    public float dashPower = 10f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
        chrWalk = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.C) && dashAvailable)
        {
            chrWalk.SetBool("isDashing", true);
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        dashAvailable = false;
        isDashing = true;
        rb.velocity = new Vector3(Math.Sign(chrTransform.localScale.x) * dashPower, 0f, 0f);
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashTime);

        trailRenderer.emitting = false;
        isDashing = false;
        chrWalk.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCooldown);
        dashAvailable = true;
    }
}