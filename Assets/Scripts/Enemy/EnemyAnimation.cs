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
