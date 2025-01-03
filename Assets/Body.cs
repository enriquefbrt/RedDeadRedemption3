using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private EnemyBehavior parentScript; // Reference to the parent script

    private void Start()
    {
        parentScript = GetComponentInParent<EnemyBehavior>();
    }

    private void OnTriggerEnter (Collider other)
    {
        parentScript.HandleBodyCollision(other);
    }
}
