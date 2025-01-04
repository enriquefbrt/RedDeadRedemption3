using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float smashThreshold;
    public float castThreshold;
    public float castTime;
    public float maxHealth;
    public float projectileOffset;
    public GameObject projectilePrefab;

    private enum MovementState { Idle, Melee, Smash, Fire, Cooldown, Cast, Hurt };
    private MovementState movementState = MovementState.Idle;
    private enum LifeState { Alive, Dead }
    private LifeState lifeState = LifeState.Alive;
    private int orientation = 1;
    public float health;
    public float nextCastTime = 0f;
    private Animator animator;
   


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        target = GameObject.Find("TestGuy");
        health = maxHealth;
    }

    
    void Update()
    {
        if (lifeState == LifeState.Alive)
        {
            if (movementState == MovementState.Idle && System.Math.Abs(transform.position.x - target.transform.position.x) >= castThreshold && nextCastTime <= Time.time)
            {
                movementState = MovementState.Cast;
            }
            else if (movementState == MovementState.Idle && System.Math.Abs(transform.position.x - target.transform.position.x) >= smashThreshold)
            {
                HandleMovement();
                UpdateOrientation();
            }
            else if (movementState == MovementState.Idle)
            {
                float k = Random.Range(0f, 3f);
                if (k >= 2)
                {
                    movementState = MovementState.Melee;
                }
                else if (k >= 1)
                {
                    movementState = MovementState.Smash;
                }
                else
                {
                    movementState = MovementState.Fire;
                }
            }
            else if (movementState == MovementState.Smash)
            {
                StartCoroutine(Smash());
                movementState = MovementState.Cooldown;
            }
            else if (movementState == MovementState.Melee)
            {
                StartCoroutine(Melee());
                movementState = MovementState.Cooldown;
            }
            else if (movementState == MovementState.Fire)
            {
                StartCoroutine(Fire());
                movementState = MovementState.Cooldown;
            }
            else if (movementState == MovementState.Cast)
            {
                StartCoroutine(Cast());
                nextCastTime = Time.time + castTime;
                movementState = MovementState.Cooldown;
            }
        }
    }

    public void OnHit(Collider2D other)
    {
        if (movementState == MovementState.Idle && other.CompareTag("Bullet"))
        {
            StartCoroutine(Hurt());
        }
    }

    private void HandleMovement()
    {
        float newX = transform.position.x - orientation * speed * Time.deltaTime;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void UpdateOrientation()
    {
        orientation = System.Math.Sign(transform.position.x - target.transform.position.x);
        Vector3 scale = transform.localScale;
        scale.x = orientation;
        transform.localScale = scale;
    }

    private IEnumerator Melee()
    {
        animator.SetTrigger("MeleeTrigger");
        yield return new WaitForSeconds(2.27f);
        movementState = MovementState.Idle;
    }

    private IEnumerator Smash()
    {
        animator.SetTrigger("SmashTrigger");
        yield return new WaitForSeconds(2.27f);
        movementState = MovementState.Idle;
    }

    private IEnumerator Fire()
    {
        animator.SetTrigger("FireTrigger");
        yield return new WaitForSeconds(2.5f);
        movementState = MovementState.Idle;
    }

    private IEnumerator Cast()
    {
        animator.SetTrigger("CastTrigger");
        yield return new WaitForSeconds(0.5f); //Half animation
        foreach (float y in new float[] { 0.5f, 1.5f, 4f, 5f}) 
        {
            float height = Random.Range(0.2f, 5f); 
            Vector3 spawnPoint = new(transform.position.x - projectileOffset*orientation, height, transform.position.z);
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint, Quaternion.identity);
            DemonProyectileBehavior demonProyectileBehavior = projectile.GetComponent<DemonProyectileBehavior>();
            demonProyectileBehavior.orientation = orientation;
            yield return new WaitForSeconds(0.025f); //Rest of animation
        }
        yield return new WaitForSeconds(0.5f); //Rest of animation
        movementState = MovementState.Idle;
    }

    private IEnumerator Hurt()
    {
        health -= 1;
        movementState = MovementState.Hurt;
        animator.SetTrigger("HurtTrigger");
        yield return new WaitForSeconds(0.7f);
        if (health > 0)
        {
            movementState = MovementState.Idle;
        }
        else
        {
            lifeState = LifeState.Dead;
            animator.SetTrigger("DeathTrigger");
            yield return new WaitForSeconds(3.2f);
            Destroy(gameObject);
        }
    }
}
