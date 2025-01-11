using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonProyectileFactory : Factory
{

    [SerializeField] GameObject demonProyectilePrefab;

    public GameObject CreateDemonProyectile(Vector3 spawnPoint) {
        return Instantiate(demonProyectilePrefab, spawnPoint, Quaternion.identity);
    }
}
