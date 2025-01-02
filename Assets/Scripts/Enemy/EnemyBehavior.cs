using UnityEngine;
using System.Collections;     

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 1f;      
    public float amplitude = 3f;  
    public float attackCooldown = 2f;
    public int maxLife;
    public GameObject fireballPrefab;

    private enum EnemyState { Fly, Cooldown, Dying }
    private int life;
    private EnemyState currentState;
    private Vector3 startPosition;
    private EnemyAnimation enemyAnimation;


    void Start()
    {
        currentState = EnemyState.Fly;
        life = maxLife;
        startPosition = transform.position;
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
    }

    void Update()
    {
            float newX = startPosition.x + Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (currentState == EnemyState.Fly && other.CompareTag("Player"))
        {
            StartCoroutine(Attack());
            currentState = EnemyState.Cooldown;
        }

    }

    private IEnumerator Attack()
    {
        enemyAnimation.TriggerAttackAnimation();
        yield return new WaitForSeconds(enemyAnimation.GetAttackAnimationLength()/2); //middle of animation
        Instantiate(fireballPrefab, transform.position - new Vector3(0.7f, 0f, 0f), Quaternion.identity);
        yield return new WaitForSeconds(attackCooldown);
        currentState = EnemyState.Fly;
    }
}

