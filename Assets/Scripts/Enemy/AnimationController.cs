using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();  // Get the Animator component
    }

    void Update()
    {
        // Example: Trigger the attack animation when the player presses the "Space" key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("AttackTrigger");  // Trigger the Attack animation
        }
    }
}
