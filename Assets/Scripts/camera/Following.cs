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
    public float zDistance;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 chrPosition = chrTransform.position;
        int orientation = walkClass.GetCharacterOrientation(chrTransform);
        cameraTransform.position = new Vector3(chrPosition.x + displacement * orientation,
            chrPosition.y, chrPosition.z - zDistance);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 chrPosition = chrTransform.position;
        int orientation = walkClass.GetCharacterOrientation(chrTransform);
        float targetX = chrPosition.x + displacement * orientation;
        float targetY = chrPosition.y;
        float targetZ = chrPosition.z - zDistance;

        Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);

        cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPosition, ref velocity, smoothTime);
    }
}
