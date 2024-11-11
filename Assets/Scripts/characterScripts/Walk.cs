using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public Animator characterWalk;
    public float walkingSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        characterWalk = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            characterWalk.SetBool("moving", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * walkingSpeed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            characterWalk.SetBool("moving", false);
        }
    }
}
