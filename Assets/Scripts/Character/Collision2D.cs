using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision2D : MonoBehaviour
{
    private CollisionManager parentScript;

    private void Start()
    {
        parentScript = GetComponentInParent<CollisionManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        parentScript.HandleHit(other);
    }
}