using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private JumpScript jumpScript;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public Material whiteMaterial;
    public Vector3 pushDirection = new Vector3(-1, 0, 0); // Direction of the push (X-axis by default)
    public float pushForce = 15f; // Magnitude of the force
    [SerializeField] private  float collisionCooldown = 2f;
    private float newCollisionTime = 0f;
    
    private Rigidbody2D rb;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        rb = GetComponent<Rigidbody2D>();
    }
    public void HandleHit(Collider2D other)
    {
        if (other.CompareTag("Boss") && newCollisionTime < Time.time) {
            int orientation = Math.Sign(other.transform.position.x - transform.position.x);
            rb.AddForce(pushDirection.normalized * pushForce * orientation, ForceMode2D.Impulse);
            newCollisionTime = Time.time + collisionCooldown;
        }
        if (!other.CompareTag("EnemyRange") && !other.CompareTag("EnemyBody") && !other.CompareTag("SecretTag")) {
            StartCoroutine(ChangeColorTemporarily());
            healthManager.Hurt();
        }
    }
    private IEnumerator ChangeColorTemporarily()
    {
        spriteRenderer.material = whiteMaterial;
        yield return new WaitForSeconds(0.35f);
        spriteRenderer.material = originalMaterial;
    }
}
