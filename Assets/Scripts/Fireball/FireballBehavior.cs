using UnityEngine;
using System.Collections;    

public class FireballBehavior : MonoBehaviour
{
    public float range = 10f;
    public float speed = 8f;

    private enum State { Fly, Collide, Explode };
    private State currentState;
    private Vector3 initialPosition;
    private FireballAnimation Animation;

    void Start()
    {
        Animation = GetComponentInChildren<FireballAnimation>();
        initialPosition = transform.position;
        currentState = State.Fly;
    }

    private void Update()
    {
        if (currentState == State.Fly && transform.position.x < initialPosition.x - range)
        {
            currentState = State.Collide;
        }
        else if (currentState == State.Fly)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        } 
        else if (currentState == State.Collide) 
        {
            currentState = State.Explode;
            StartCoroutine(DestroyFireball());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState == State.Fly) { currentState = State.Collide; }
    }

    private IEnumerator DestroyFireball()
    {
        Animation.TriggerExplosionAnimation();
        yield return new WaitForSeconds(0.333f);  // Adjust this to match the explosion animation's duration
        Destroy(gameObject);
    }
}
