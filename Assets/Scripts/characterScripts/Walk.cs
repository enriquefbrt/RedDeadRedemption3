using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public Animator characterWalk;
    public float walkingSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        characterWalk = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * walkingSpeed * Time.deltaTime);
            characterWalk.SetBool("moving", true);
            characterWalk.SetBool("right", false);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            characterWalk.SetBool("moving", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * walkingSpeed * Time.deltaTime);
            characterWalk.SetBool("moving", true);
            characterWalk.SetBool("right", false);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            characterWalk.SetBool("moving", false);
        }
    }
}
