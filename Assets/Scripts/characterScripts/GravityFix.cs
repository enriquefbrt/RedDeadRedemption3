using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFix : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float gravityCap = -30f;

    void Update()
    {
        if (rb.velocity.y < gravityCap)
        {
            rb.velocity = new Vector3(rb.velocity.x, gravityCap, 0f);
        }
    }
}
