using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAttackAnimation()
    {
        animator.SetTrigger("AttackTrigger");
    }

    public void TriggerHurtAnimation()
    {
        animator.SetTrigger("HurtTrigger");
    }

    public void TriggerDeathAnimation()
    {
        animator.SetTrigger("DeathTrigger");
    }

    public float GetAttackAnimationLength()
    {
        var clips = animator.runtimeAnimatorController.animationClips;

        foreach (var clip in clips)
        {
            if (clip.name == "EnemyAttack") 
            {
                return clip.length;
            }
        }

        Debug.LogWarning("Attack animation clip not found! Defaulting to 1 second.");
        return 1f; // Default duration if clip is not found
    }
}
