using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonProyectileBehavior : MonoBehaviour
{
    public float range = 10f;
    public float speed = 8f;
    public int orientation = 1;

    private enum State { Fly, Collide, Explode };
    private State state;
    private Vector3 initialPosition;
    private Animator animator;

    void Start()
    {
        initialPosition = transform.position;
        state = State.Fly;
        animator = GetComponent<Animator>();
        UpdateOrientation();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Fly && transform.position.x * orientation < initialPosition.x * orientation - range)
        {
            state = State.Collide;
        }
        else if (state == State.Fly)
        {
            transform.position += orientation * speed * Time.deltaTime * Vector3.left;
        }
        else if (state == State.Collide)
        {
            state = State.Explode;
            StartCoroutine(DestroyProyectile());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (state == State.Fly) { state = State.Collide; }
    }

    private IEnumerator DestroyProyectile()
    {
        animator.SetTrigger("ExplodeTrigger");
        yield return new WaitForSeconds(0.333f);  // Adjust this to match the explosion animation's duration
        Destroy(gameObject);
    }

    private void UpdateOrientation()
    {
        Vector3 scale = transform.localScale;
        scale.x = orientation;
        transform.localScale = scale;
    }

}
