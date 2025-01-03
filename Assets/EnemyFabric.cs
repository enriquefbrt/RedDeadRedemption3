using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFabric : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float positionX;
    public float positionY;
    public float positionZ;

    void Start()
    {
        Instantiate(EnemyPrefab, new Vector3(positionX, positionY, positionZ), Quaternion.identity);
    }

}
