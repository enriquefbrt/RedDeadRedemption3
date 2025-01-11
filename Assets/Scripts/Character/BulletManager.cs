using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float speed = 30f;

    private GameObject character;
    private Transform chrTransform;
    private int orientation;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindWithTag("ChrSprite");
        chrTransform = character.transform;
        orientation = Math.Sign(chrTransform.localScale.x);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 translatePoint = Vector3.right * orientation * speed * Time.deltaTime;
        translatePoint.z = 0f;
        transform.Translate(translatePoint);
        Destroy(gameObject, 2f);
    }
}
