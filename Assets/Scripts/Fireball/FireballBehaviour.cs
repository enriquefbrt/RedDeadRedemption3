using UnityEngine;
using System.Collections;    

public class FireballBehavior : MonoBehaviour
{
    private FireballAnimation Animation;  
    private Transform enemyTransform;
    public bool isFlying;

    public float flightTime = 2f;
    public float range = 10f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        Animation = GetComponentInChildren<FireballAnimation>();
        enemyTransform = transform.parent;
        isFlying = false;

        initialPosition = transform.parent.position - new Vector3(0.7f, 0f, 0f);  //Not right on the monster
        targetPosition = initialPosition - new Vector3(range, 0f, 0f);  // Fly forward on X-axis
        StartCoroutine(FlyFireball());
    }


    private IEnumerator FlyFireball()
    {
        isFlying = true;

        float elapsedTime = 0f;
        while (elapsedTime < flightTime)
        {
            if (!isFlying)  
            {
                break;  
            }

            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / flightTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isFlying = false;
        StartCoroutine(ResetFireball());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFlying)  
        {
            isFlying = false;
        }
    }

    private IEnumerator ResetFireball()
    {
        Animation.TriggerExplosionAnimation();
        transform.SetParent(null, true);
        yield return new WaitForSeconds(0.333f);  // Adjust this to match the explosion animation's duration
        Destroy(gameObject);
    }
}
