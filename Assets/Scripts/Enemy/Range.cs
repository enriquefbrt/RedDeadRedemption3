using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    private EnemyBehavior parentScript; // Reference to the parent script

    private void Start()
    {
        parentScript = GetComponentInParent<EnemyBehavior>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        parentScript.HandleRangeStay(other);
    }
}
