using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBulletFactory : Factory
{
    [SerializeField] GameObject normalBulletPrefab;

    public GameObject CreateNormalBullet(Vector3 spawnPoint) {
        return Instantiate(normalBulletPrefab, spawnPoint, Quaternion.identity);
    }
}
