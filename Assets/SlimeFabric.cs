using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFabric : MonoBehaviour
{
    public GameObject SlimePrefab;
    public float positionX;
    public float positionY;
    public float positionZ;

    void Start()
    {
        Instantiate(SlimePrefab, new Vector3(positionX, positionY, positionZ), Quaternion.identity);
    }

}
