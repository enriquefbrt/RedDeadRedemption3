using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject normalBullet;
    [SerializeField] private GameObject magicBullet;
    [SerializeField] private Walk walkClass;
    [SerializeField] private Transform chrTransform;

    [SerializeField] private float x = 1.23f;
    [SerializeField] private float y = 0.08f;
    [SerializeField] private float normalCooldown = 0.5f;
    [SerializeField] private float magicCooldown = 1f;
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
