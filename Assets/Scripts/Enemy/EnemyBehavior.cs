using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 5f;  
    public float amplitude = 1.5f;
    public float amplitudeY = 0.07f;
    public float frequence = 0.7f;
    public float frequenceY = 3f;
    public float attackCooldown = 2f;
    public int maxHealth;
    public int orientation = 1;
    public float pursuitThreshold = 5;
    public Vector3 fireballOffset = new(0.7f, 0f, 0f);
    public GameObject fireballPrefab;

    private enum AtackState { Ready, Cooldown };
    private AtackState atackState = AtackState.Ready;
    private enum MovementState { Idle, Following } 
    private MovementState movementState = MovementState.Idle;
    private enum LifeState { Alive, Dying, Dead };
    private LifeState lifeState = LifeState.Alive;
    public int health;
    private Transform target;
    private EnemyAnimation enemyAnimation;
    private float timeOffset;


    void Start()
    {
        health = maxHealth;
        timeOffset = Time.time;
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
    }

    void Update()
    {
        if (lifeState == LifeState.Alive)
        {
            HandleMovement();
            UpdateOrientation();
            if (health <= 0)
            {
                lifeState = LifeState.Dying;
            }
        }
        else if (lifeState == LifeState.Dying)
        {
            StartCoroutine(Die());
            lifeState = LifeState.Dead;
        }
    }

    public void HandleRangeStay(Collider other)
    {
        if (lifeState == LifeState.Alive && other.CompareTag("Player"))
        {
            float distance = transform.position.x - other.transform.position.x;
            orientation = System.Math.Sign(distance);
            if (movementState == MovementState.Idle && System.Math.Abs(distance) < pursuitThreshold)
            {
                movementState = MovementState.Following;
                target = other.transform;
            }
            if (atackState == AtackState.Ready)
            {
                StartCoroutine(Attack());
                atackState = AtackState.Cooldown;
            }
        }
    }

    public void HandleBodyCollision(Collider other)
    {
        if (lifeState == LifeState.Alive && other.CompareTag("Player"))
        {
            health -= 1;
            if (health > 0)
            {
                enemyAnimation.TriggerHurtAnimation();
            }
        }
    }

    private void HandleMovement()
    {
        Vector3 position = transform.position;
        if (movementState == MovementState.Following)
        {
            float directionX = target.position.x - transform.position.x;
            if (System.Math.Abs(directionX) > 8)
            {
                movementState = MovementState.Idle;
                timeOffset = Time.time;
            }
            else if (System.Math.Abs(directionX) > 2)
            {
                position.x -= orientation * speed * Time.deltaTime;
                //position.y = target.position.y;
            }
        }
        else
        {
            position.x = transform.position.x - Mathf.Cos((Time.time - timeOffset) * frequence) * amplitude * orientation * Time.deltaTime;  //sin(x) = integral(cos(x))
        }
        position.y -= Mathf.Cos((Time.time - timeOffset) * frequenceY) * amplitudeY * orientation * Time.deltaTime;
        transform.position = position;
    }

    private void UpdateOrientation()
    {
        Vector3 scale = transform.localScale;
        scale.x = orientation;
        transform.localScale = scale;
    }

    private IEnumerator Attack()
    {
        enemyAnimation.TriggerAttackAnimation();
        yield return new WaitForSeconds(enemyAnimation.GetAttackAnimationLength()/2); //middle of animation
        GameObject fireball = Instantiate(fireballPrefab, transform.position - fireballOffset*orientation, Quaternion.identity);
        FireballBehavior fireballBehavior = fireball.GetComponent<FireballBehavior>();
        fireballBehavior.orientation = orientation;
        yield return new WaitForSeconds(attackCooldown);
        atackState = AtackState.Ready;
    }

    private IEnumerator Hurt()
    {
        enemyAnimation.TriggerHurtAnimation();
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator Die()
    {
        enemyAnimation.TriggerDeathAnimation();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

