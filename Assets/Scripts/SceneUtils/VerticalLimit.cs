using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLimit : MonoBehaviour
{

    public event Action OnLimitReached;
    private void OnTriggerStay2D(Collider2D collision) {
        OnLimitReached?.Invoke();
    }
}
