using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using System;

public class BulletCollisionManager : MonoBehaviour
{
    [SerializeField] private bool isBulletMagic;
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
        if (other.CompareTag("Ground"))
        {
            if (isBulletMagic)
            {
                Vector2 impactPoint = other.ClosestPoint(transform.position);
                int impactOrientation = Math.Sign(chrTransform.localScale.x);
                characterRoot.transform.position = new Vector3(impactPoint.x - 1 * impactOrientation, impactPoint.y, 0f);
            }
            Destroy(gameObject);
        } 
    }
}
