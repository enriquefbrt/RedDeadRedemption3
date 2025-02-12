using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using System;

public class BulletCollisionManager : MonoBehaviour
{
    [SerializeField] private bool isBulletMagic;
    private Rigidbody2D rb;
    private GameObject characterRoot;

    void Start()
    {
        characterRoot = GameObject.FindWithTag("Player");
        rb = characterRoot.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            if (isBulletMagic)
            {
                Vector2 impactPoint = other.ClosestPoint(transform.position);
                int impactOrientation = Math.Sign(impactPoint.x - characterRoot.transform.position.x);
                characterRoot.transform.position = new Vector3(impactPoint.x - 1 * impactOrientation, impactPoint.y, 0f);
                rb.velocity = new Vector3(0f, 0f, 0f);
            }
            Destroy(gameObject);
        } 
    }
}
