using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shoot : MonoBehaviour
{
    public GameObject normalBullet;
    public GameObject magicBullet;
    public Walk walkClass;
    public Transform chrTransform;

    public float x = 1.23f;
    public float y = 0.08f;
    public float normalCooldown = 2f;
    private float magicCooldown = 2f;
    private float currentNormalCooldown = 0.5f;
    private float currentMagicCooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentNormalCooldown > normalCooldown)
        {
            currentNormalCooldown = 0f;
            GameObject bullet = Instantiate(normalBullet, transform.position, transform.rotation);
            bullet.transform.position = BulletInitialPosition();
            Destroy(bullet, 1f);
        }

        if (Input.GetMouseButtonDown(1) && currentMagicCooldown > magicCooldown)
        {
            currentMagicCooldown = 0f;
            GameObject bullet = Instantiate(magicBullet, transform.position, transform.rotation);
            bullet.transform.position = BulletInitialPosition();
            Destroy(bullet, 1f);
        }

        currentNormalCooldown += Time.deltaTime;
        currentMagicCooldown += Time.deltaTime;
    }

    private Vector3 BulletInitialPosition()
    {
        int orientation = walkClass.GetCharacterOrientation(chrTransform);
        return new Vector3(transform.position.x + orientation * x, transform.position.y + y, transform.position.z);
    }
}
