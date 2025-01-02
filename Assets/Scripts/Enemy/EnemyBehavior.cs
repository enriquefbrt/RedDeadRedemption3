using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 5f;  
    public float amplitude = 3f;
    public float amplitudeY = 0.07f;
    public float frequence = 0.7f;
    public float frequenceY = 3f;
    public float attackCooldown = 2f;
    public int maxHealth;
    public int orientation = 1;
    public float pursuitThreshold = 5;
    public Vector3 fireballOffset = new Vector3(0.7f, 0f, 0f);
    public GameObject fireballPrefab;

    private enum EnemyState { Fly, Cooldown, Dying };
    private EnemyState currentState;
    private int health;
    public bool isFollowing= false;
    private Transform target;
    private Vector3 startPosition;
    private EnemyAnimation enemyAnimation;
    private float timeOffset = 0f;


    void Start()
    {
        currentState = EnemyState.Fly;
        health = maxHealth;
        startPosition = transform.position;
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
    }

    void Update()
    {
        if (isFollowing)
        {
            Vector3 direction = target.position - transform.position;
            if (System.Math.Abs(direction.x) > 8)
            {
                isFollowing = false;
                startPosition = transform.position;
                timeOffset = Time.time;
            }
            else
            {
                orientation = -1 * System.Math.Sign(direction.x);
                direction.Normalize();
                transform.position += direction * speed * Time.deltaTime;
            }
        }
        else
        {
            float newX = startPosition.x + Mathf.Sin((Time.time - timeOffset)*frequence) * amplitude * orientation * (-1);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        float newY = startPosition.y + Mathf.Sin((Time.time - timeOffset) * frequenceY) * amplitudeY * orientation * (-1);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        UpdateOrientation();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float distance = transform.position.x - other.transform.position.x;
            orientation = System.Math.Sign(distance);
            if (System.Math.Abs(distance) < pursuitThreshold)
            {
                isFollowing = true;
                target = other.transform;
            }
            if (currentState == EnemyState.Fly)
            {
                StartCoroutine(Attack());
                currentState = EnemyState.Cooldown;
            }
        }

    }

    private IEnumerator Attack()
    {
        enemyAnimation.TriggerAttackAnimation();
        yield return new WaitForSeconds(enemyAnimation.GetAttackAnimationLength()/2); //middle of animation
        GameObject fireball = Instantiate(fireballPrefab, transform.position - fireballOffset*orientation, Quaternion.identity);
        FireballBehavior fireballBehavior = fireball.GetComponent<FireballBehavior>();
        fireballBehavior.orientation = orientation;
        yield return new WaitForSeconds(attackCooldown);
        currentState = EnemyState.Fly;
    }

    private void UpdateOrientation()
    {
        Vector3 scale = transform.localScale;
        scale.x = orientation;
        transform.localScale = scale;
    }
}

