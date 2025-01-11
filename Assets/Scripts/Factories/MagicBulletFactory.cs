using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBulletFactory : Factory
{
    [SerializeField] GameObject magicBulletPrefab;

    public GameObject CreateMagicBullet(Vector3 spawnPoint) {
        return Instantiate(magicBulletPrefab, spawnPoint, Quaternion.identity);
    }
}
