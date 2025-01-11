using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float movementRange;
    [SerializeField] private float speed;
    [SerializeField] private GameObject BossPrefab;
    public event Action OnDeath;

    private enum State { Idle, Dying, Dead };
    private State state = State.Idle;
    private float health;
    private Vector3 startPosition;
    private int orientation = 1;
    private Animator animator;

    void Awake()
    {
        health = maxHealth;
        startPosition = transform.position;
        animator = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        if (state == State.Idle)
        {
            if (System.Math.Abs(startPosition.x - transform.position.x) > movementRange)
            {
                orientation *= -1;
            }
            float newX = transform.position.x - orientation * speed * Time.deltaTime;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            UpdateOrientation();
        }
        else if (state == State.Dying) 
        {
            StartCoroutine(Transform());
            state = State.Dead;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) {
            health -= 1;
            animator.SetTrigger("HurtTrigger");
            if (health <= 0) {
                state = State.Dying;
            }
        }
    }

    private void UpdateOrientation()
    {
        Vector3 scale = transform.localScale;
        scale.x = orientation;
        transform.localScale = scale;
    }

    private IEnumerator Transform()
    {
        orientation = 1;
        UpdateOrientation();
        animator.SetTrigger("TransformTrigger");
        yield return new WaitForSeconds(5f);
        OnDeath?.Invoke();
        Instantiate(BossPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
