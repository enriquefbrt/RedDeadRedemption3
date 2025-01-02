using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Orientator : MonoBehaviour
{// Start is called before the first frame update

    public int orientation = 1;

    public void UpdateOrientation()
    {
        Vector3 scale = transform.localScale;
        scale.x = orientation;
        transform.localScale = scale;
    }

}
