using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using System;

public class MagicBulletCollisionManager : MonoBehaviour
{
    private GameObject characterRoot;
    private GameObject character;
    private Transform chrTransform;

    void Start()
    {
        character = GameObject.FindWithTag("ChrSprite");
        characterRoot = GameObject.FindWithTag("Player");
        chrTransform = character.transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hola");
        if (other.CompareTag("Ground"))
        {
            Vector3 impactPoint = other.ClosestPoint(transform.position);
            int impactOrientation = Math.Sign(chrTransform.localScale.x);
            Vector3 teleportPoint = new Vector3(impactPoint.x - 1 * impactOrientation, impactPoint.y, impactPoint.z);
            characterRoot.transform.position = teleportPoint;
            Destroy(gameObject);
        } 
    }
}
