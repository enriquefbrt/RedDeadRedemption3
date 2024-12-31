using UnityEngine;
using System.Collections;

public class FireballBehavior : MonoBehaviour
{
    private FireballAnimation Animation;  // Reference to the Animator on the FireballAnimator child
    public bool isFlying = false;
    private Transform enemyTransform;

    void Start()
    {
        Animation = GetComponentInChildren<FireballAnimation>();
        Animation.hide();
        enemyTransform = transform.parent;
    }

    // Method to trigger the activation of the fireball and start the flying animation
    public void ActivateFireball()
    {
        if (isFlying) return;  

        StartCoroutine(FlyFireball());
    }

    // Coroutine to simulate the fireball flying
    private IEnumerator FlyFireball()
    {
        Animation.appear();
        isFlying = true;
        float flightTime = 2f;  // How long the fireball will fly
        Vector3 initialPosition = transform.parent.position - new Vector3(0.75f,0f,0f);  //Not right on the monster
        Vector3 targetPosition = initialPosition - new Vector3(10f, 0f, 0f);  // Fly forward on X-axis


        float elapsedTime = 0f;
        while (elapsedTime < flightTime)
        {
            if (!isFlying)  // Check if isFlying is false
            {
                break;  // Exit the loop if isFlying becomes false
            }

            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / flightTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        if (isFlying) 
        {
            StartCoroutine(ResetFireball());
        }
    }

    // Detect collision with player or other objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isFlying)  // Change "Player" tag as needed
        {
            StartCoroutine(ResetFireball());
        }
    }

    // Coroutine to reset the fireball after explosion
    private IEnumerator ResetFireball()
    {
        // Wait for the explosion animation to finish (adjust the time based on animation length)
        Animation.TriggerExplosionAnimation();
        isFlying = false;
        transform.SetParent(null, true);
        yield return new WaitForSeconds(0.333f);  // Adjust this to match the explosion animation's duration
        transform.SetParent(enemyTransform);
        transform.position = enemyTransform.position;
        // Reset the fireball's position and set it to inactive (hidden at monster's position)
        Animation.hide();
    }
}
