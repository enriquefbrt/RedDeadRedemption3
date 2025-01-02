using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public float maxHealth;
    public float damage;
    public float speed;

    private enum State { Idle, Attack, Cooldown, Dying}
    private State currentState;
    private float health;

    void Start()
    {
        currentState = State.Idle;
        health = maxHealth;
    }

    void Update()
    {
        
    }
}
