using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAnimation : MonoBehaviour     
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerExplosionAnimation()
    {
        animator.SetTrigger("ExplosionTrigger");
    }

    public void hide()
    {
        this.GetComponent<Renderer>().enabled = false;
    }

    public void appear()
    {
        this.GetComponent<Renderer>().enabled = true;
    }
}
