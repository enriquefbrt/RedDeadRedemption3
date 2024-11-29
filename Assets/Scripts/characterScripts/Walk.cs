using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public Animator chrWalk;

    public Transform chrTransform;
    public Transform gunTransform;

    public GameObject gun;
    private GunStart _gunStart;

    public float walkingSpeed = 3f;
    private bool _right = true;
    private bool _isFlipped = false;
    private float _gunX;

    // Start is called before the first frame update
    void Start()
    {
        chrWalk = GetComponentInChildren<Animator>();
        _gunStart = gun.GetComponent<GunStart>();

        _gunX = _gunStart.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * walkingSpeed * Time.deltaTime);
            chrWalk.SetBool("moving", true);
            if (!_right)
            {
                _right = true;
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
            if (_right)
            {
                _right = false;
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

        _isFlipped = !_isFlipped;

        if (_isFlipped) { gunTransform.Translate(new Vector3(-2*_gunX, 0, 0)); }
        else { gunTransform.Translate(new Vector3(2 * _gunX, 0, 0)); }
    }
}
