using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float speed = 30f;
    public bool isBulletMagic;

    private GameObject character;
    private GameObject characterRoot;
    private Transform chrTransform;
    private int orientation;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindWithTag("ChrSprite");
        characterRoot = GameObject.FindWithTag("Player");
        chrTransform = character.transform;
        orientation = Math.Sign(chrTransform.localScale.x);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * orientation * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();


            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            if (isBulletMagic)
            {
                Vector3 impactPoint = other.ClosestPoint(transform.position);
                int impactOrientation = Math.Sign(chrTransform.localScale.x);
                Vector3 teleportPoint = new Vector3(impactPoint.x - 1 * impactOrientation, impactPoint.y, impactPoint.z);
                characterRoot.transform.position = teleportPoint;

            }
            Destroy(gameObject);
        }
    }
}
