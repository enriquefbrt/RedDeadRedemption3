using UnityEngine;
using System.Collections;    

public class FireballBehavior : MonoBehaviour
{
    public float range = 10f;
    public float speed = 8f;
    public int orientation = 1;

    private enum State { Fly, Collide, Explode };
    private State currentState;
    private Vector3 initialPosition;
    private FireballAnimation Animation;

    void Start()
    {
        Animation = GetComponentInChildren<FireballAnimation>();
        initialPosition = transform.position;
        currentState = State.Fly;
        UpdateOrientation();
    }

    private void Update()
    {
        if (currentState == State.Fly && transform.position.x*orientation < initialPosition.x*orientation - range)
        {
            currentState = State.Collide;
        }
        else if (currentState == State.Fly)
        {
            transform.position += orientation * speed * Time.deltaTime * Vector3.left;
        } 
        else if (currentState == State.Collide) 
        {
            currentState = State.Explode;
            StartCoroutine(DestroyFireball());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentState == State.Fly && !other.CompareTag("EnemyBody") && !other.CompareTag("EnemyRange") && !other.CompareTag("Player")) { currentState = State.Collide; }
    }

    private IEnumerator DestroyFireball()
    {
        Animation.TriggerExplosionAnimation();
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
