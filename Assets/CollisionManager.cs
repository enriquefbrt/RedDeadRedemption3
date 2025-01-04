using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public Vector3 pushDirection = new Vector3(-1, 0, 0); // Direction of the push (X-axis by default)
    public float pushForce = 10f; // Magnitude of the force
    
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void HandleHit(Collider2D other)
    {   
        int orientation = System.Math.Sign(other.transform.position.x - transform.position.x);
        rb.AddForce(pushDirection.normalized * pushForce * orientation, ForceMode.Impulse);
    }
}
