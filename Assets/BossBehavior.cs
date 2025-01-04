using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float smashThreshold;

    private enum MovementState { Idle, Melee, Smash, Fire, Cooldown };
    private MovementState movementState = MovementState.Idle;
    private int orientation = 1;
    private Animator animator;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        target = GameObject.Find("CharacterRoot");
    }

    
    void Update()
    {
        if (movementState == MovementState.Idle && System.Math.Abs(transform.position.x - target.transform.position.x) >= smashThreshold)
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
            }else
            {
                movementState=MovementState.Fire;
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
            movementState= MovementState.Cooldown;
        }
        else if (movementState == MovementState.Fire)
        {
            StartCoroutine(Fire());
            movementState = MovementState.Cooldown;
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
}
