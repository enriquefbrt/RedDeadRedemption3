using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float speed = 30f;

    private GameObject character;
    private GameObject characterRoot;
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
        transform.Translate(Vector3.right * orientation * speed * Time.deltaTime);
        Destroy(gameObject, 1f);
    }
}
