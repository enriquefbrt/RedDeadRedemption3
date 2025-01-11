using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Following : MonoBehaviour
{
    [SerializeField] private Transform chrTransform;
    [SerializeField] private Walk walkClass;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private float displacement = 6f;
    [SerializeField] private float zDistance;
    [SerializeField] private SlimeBehavior slimeBehavior;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 chrPosition = chrTransform.position;
        int orientation = walkClass.GetCharacterOrientation(chrTransform);
        transform.position = new Vector3(chrPosition.x + displacement * orientation,
            chrPosition.y, chrPosition.z - zDistance);

        slimeBehavior.OnDeath += UpdateDistance;
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

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void UpdateDistance() {
        zDistance = 9.5f;
    }
}
