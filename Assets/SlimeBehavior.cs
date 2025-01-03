using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    public float maxHealth;
    public float movementRange;
    public float speed;
    public GameObject BossPrefab;

    private enum State { Idle, Dying, Dead };
    private State state = State.Idle;
    private float health;
    private Vector3 startPosition;
    private int orientation = 1;
    private Animator animator;

    void Start()
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

    private void OnTriggerEnter(Collider other)
    {
        health -= 1;
        animator.SetTrigger("HurtTrigger"); 
        if (health <= 0)
        {
            state = State.Dying;
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
        Instantiate(BossPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
