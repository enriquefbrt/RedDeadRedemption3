using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed = 1f;      
    public float amplitude = 3f;  
    public float attackCooldown = 2f; 

    private enum EnemyState { Fly, Cooldown, Dying }
    private EnemyState currentState;

    private EnemyAnimation enemyAnimation;
    private Vector3 startPosition;

    private FireballBehavior fireballBehavior;

    void Start()
    {
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        startPosition = transform.position;
        currentState = EnemyState.Fly; 
        fireballBehavior = GetComponentInChildren<FireballBehavior>();
    }

    void Update()
    {
            float newX = startPosition.x + Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentState == EnemyState.Fly)
        {
            currentState = EnemyState.Cooldown;
            StartCoroutine(HandleAttack());
        }

    }

    private IEnumerator HandleAttack()
    {
        enemyAnimation.TriggerAttackAnimation();
        yield return new WaitForSeconds(enemyAnimation.GetAttackAnimationLength()/2); //middle of animation
        fireballBehavior.ActivateFireball();
        yield return new WaitForSeconds(attackCooldown);
        currentState = EnemyState.Fly;
    }
}

