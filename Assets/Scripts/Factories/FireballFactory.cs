using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballFactory : Factory
{
    [SerializeField] GameObject fireballPrefab;

    public GameObject CreateFireball(Vector3 spawnPoint) {
        return Instantiate(fireballPrefab, spawnPoint, Quaternion.identity);
    }
}
