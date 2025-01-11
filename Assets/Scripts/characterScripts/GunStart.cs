using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStart : MonoBehaviour
{
    [SerializeField] private float x = 0.593f;
    [SerializeField] private float y = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = transform.parent;

        // Modify the parent's position
        transform.position = parentTransform.position + new Vector3(x, y, 0f);
    }

    public float X => x;
}
