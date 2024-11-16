using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public Animator characterWalk;
    public Transform child;
    public float walkingSpeed = 3f;
    private bool _right = true;

    // Start is called before the first frame update
    void Start()
    {
        characterWalk = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * walkingSpeed * Time.deltaTime);
            characterWalk.SetBool("moving", true);
            if (!_right)
            {
                _right = true;
                FlipChild();
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            characterWalk.SetBool("moving", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * walkingSpeed * Time.deltaTime);
            characterWalk.SetBool("moving", true);
            if (_right)
            {
                _right = false;
                FlipChild();
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            characterWalk.SetBool("moving", false);
        }
    }

    void FlipChild()
    {
        Vector3 scale = child.localScale;
        scale.x *= -1;
        child.localScale = scale;
    }
}
