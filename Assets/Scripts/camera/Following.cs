using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Following : MonoBehaviour
{
    public Transform chrTransform;
    public Transform cameraTransform;
    public Walk walkClass;
    public float smoothTime = 0.1f;
    public float displacement = 6f;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 chrPosition = chrTransform.position;
        int orientation = walkClass.GetCharacterOrientation(chrTransform);
        cameraTransform.position = new Vector3(chrPosition.x + displacement * orientation,
            chrPosition.y + 2, chrPosition.z - 10);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 chrPosition = chrTransform.position;
        int orientation = walkClass.GetCharacterOrientation(chrTransform);
        float targetX = chrPosition.x + displacement * orientation;
        float targetY = chrPosition.y + 2;
        float targetZ = chrPosition.z - 10;

        Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);

        cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPosition, ref velocity, smoothTime);
    }
}
