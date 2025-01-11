using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private DemonProyectileFactory demonProyectileFactory;
    [SerializeField] private float speed;
    [SerializeField] private float smashThreshold;
    [SerializeField] private float castThreshold;
    [SerializeField] private float castTime;
    [SerializeField] private float maxHealth;
    [SerializeField] private float projectileOffset;
    public static event Action OnBossDeath;

    private enum State { Idle, Melee, Smash, Fire, Cooldown, Cast, Hurt, Dead };
    private State state = State.Idle;
    private int orientation = 1;
    private float health;
    private float nextCastTime = 0f;
    private Animator animator;
   


    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        health = maxHealth;
    }

    private void Start() {
        target = GameObject.Find("CharacterRoot");
        UpdateOrientation();
    }


    void Update()
    {
        if (state == State.Idle && System.Math.Abs(transform.position.x - target.transform.position.x) >= castThreshold && nextCastTime <= Time.time)
        {
            state = State.Cast;
            UpdateOrientation();
        }
        else if (state == State.Idle && System.Math.Abs(transform.position.x - target.transform.position.x) >= smashThreshold)
        {
            HandleMovement();
            UpdateOrientation();
        }
        else if (state == State.Idle)
        {
            float k = UnityEngine.Random.Range(0f, 3f);
            if (k >= 2)
            {
                state = State.Melee;
            }
            else if (k >= 1)
            {
                state = State.Smash;
            }
            else
            {
                state = State.Fire;
            }
        }
        else if (state == State.Smash)
        {
            StartCoroutine(Smash());
            state = State.Cooldown;
        }
        else if (state == State.Melee)
        {
            StartCoroutine(Melee());
            state = State.Cooldown;
        }
        else if (state == State.Fire)
        {
            StartCoroutine(Fire());
            state = State.Cooldown;
        }
        else if (state == State.Cast)
        {
            StartCoroutine(Cast());
            nextCastTime = Time.time + castTime;
            state = State.Cooldown;
        } 
    }

    public void OnHit(Collider2D other)
    {
        if (state == State.Idle && other.CompareTag("Bullet"))
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
        state = State.Idle;
    }

    private IEnumerator Smash()
    {
        animator.SetTrigger("SmashTrigger");
        yield return new WaitForSeconds(2.27f);
        state = State.Idle;
    }

    private IEnumerator Fire()
    {
        animator.SetTrigger("FireTrigger");
        yield return new WaitForSeconds(2.5f);
        state = State.Idle;
    }

    private IEnumerator Cast()
    {
        animator.SetTrigger("CastTrigger");
        yield return new WaitForSeconds(0.5f); //Half animation
        foreach (float y in new float[] { 0.5f, 1.5f, 4f, 5f}) 
        {
            float height = UnityEngine.Random.Range(-0.2f, 3f); 
            Vector3 spawnPoint = new(transform.position.x - projectileOffset*orientation, transform.position.y + height, transform.position.z);
            GameObject projectile = demonProyectileFactory.CreateDemonProyectile(spawnPoint);
            DemonProyectileBehavior demonProyectileBehavior = projectile.GetComponent<DemonProyectileBehavior>();
            demonProyectileBehavior.orientation = orientation;
            yield return new WaitForSeconds(0.025f); //Rest of animation
        }
        yield return new WaitForSeconds(0.5f); //Rest of animation
        state = State.Idle;
    }

    private IEnumerator Hurt()
    {
        health -= 1;
        state = State.Hurt;
        animator.SetTrigger("HurtTrigger");
        yield return new WaitForSeconds(0.4667f);
        if (health > 0)
        {
            state = State.Idle;
        }
        else
        {
            state = State.Dead;
            animator.SetTrigger("DeathTrigger");
            yield return new WaitForSeconds(3.2f);
            OnBossDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
