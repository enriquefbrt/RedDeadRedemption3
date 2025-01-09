using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int health;
    private float cooldown = 1f;
    private float cooldownTime = 0f;
    public event Action OnPlayerDeath;

    void Start()
    {
        health = 9;
        HealthDisplay.Instance.UpdateHealth(health);
    }

    public void Hurt()
    {
        if (cooldownTime <= Time.time)
        {
            health -= 1;
            HealthDisplay.Instance.UpdateHealth(health);
            cooldownTime = Time.time + cooldown;
        }
        if (health <= 0) { 
            OnPlayerDeath?.Invoke();
        }
    }
}
