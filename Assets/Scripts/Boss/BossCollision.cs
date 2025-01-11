using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollision : MonoBehaviour
{
    private BossBehavior parentScript; // Reference to the parent script

    private void Start()
    {
        parentScript = GetComponentInParent<BossBehavior>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        parentScript.OnHit(other);
    }
}
