using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    [SerializeField] private Animator chrWalk;

    [SerializeField] private Transform chrTransform;
    [SerializeField] private Transform gunTransform;

    [SerializeField] private GameObject gun;
    private GunStart gunStart;

    [SerializeField] private float walkingSpeed = 3f;
    private bool right = true;
    private bool isFlipped = false;
    private float gunX;

    // Start is called before the first frame update
    void Start()
    {
        chrWalk = GetComponentInChildren<Animator>();
        gunStart = gun.GetComponent<GunStart>();

        gunX = gunStart.X;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * walkingSpeed * Time.deltaTime);
            chrWalk.SetBool("moving", true);
            if (!right)
            {
                right = true;
                FlipChild();
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            chrWalk.SetBool("moving", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * walkingSpeed * Time.deltaTime);
            chrWalk.SetBool("moving", true);
            if (right)
            {
                right = false;
                FlipChild();
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            chrWalk.SetBool("moving", false);
        }
    }

    void FlipChild()
    {
        Vector3 chrScale = chrTransform.localScale;
        chrScale.x *= -1;
        chrTransform.localScale = chrScale;

        Vector3 gunScale = gunTransform.localScale;
        gunScale.x *= -1;
        gunTransform.localScale = gunScale;

        isFlipped = !isFlipped;

        if (isFlipped) { gunTransform.Translate(new Vector3(-2 * gunX, 0, 0)); }
        else { gunTransform.Translate(new Vector3(2 * gunX, 0, 0)); }
    }

    public int GetCharacterOrientation(Transform chrTransform)
    {
        return Math.Sign(chrTransform.localScale.x);
    }
}
