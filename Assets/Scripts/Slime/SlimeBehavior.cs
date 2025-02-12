using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float movementRange;
    [SerializeField] private float speed;
    [SerializeField] private GameObject BossPrefab;
    private GameObject bossMusicObject;
    private AudioSource bossMusic;
    public event Action OnDeath;

    private enum State { Idle, Dying, Dead };
    private State state = State.Idle;
    private float health;
    private Vector3 startPosition;
    private int orientation = 1;
    private float fadeDurationIn = 1f;
    private float originalVolume;
    private Animator animator;

    void Awake()
    {
        bossMusicObject = GameObject.Find("bossMusic");
        bossMusic = bossMusicObject.GetComponent<AudioSource>();
        originalVolume = bossMusic.volume;
        health = maxHealth;
        startPosition = transform.position;
        animator = GetComponentInChildren<Animator>();
        bossMusic.Play();
        bossMusic.Pause();
    }

    void Update()
    {
        if (state == State.Idle)
        {
            if (System.Math.Abs(startPosition.x - transform.position.x) > movementRange)
            {
                orientation *= -1;
            }
            float newX = transform.position.x - orientation * speed * Time.deltaTime;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            UpdateOrientation();
        }
        else if (state == State.Dying) 
        {
            StartCoroutine(Transform());
            state = State.Dead;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) {
            health -= 1;
            animator.SetTrigger("HurtTrigger");
            if (health <= 0) {
                state = State.Dying;
            }
        }
    }

    private void UpdateOrientation()
    {
        Vector3 scale = transform.localScale;
        scale.x = orientation;
        transform.localScale = scale;
    }

    private IEnumerator Transform()
    {
        orientation = 1;
        UpdateOrientation();
        animator.SetTrigger("TransformTrigger");
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(FadeInVolume());
        yield return new WaitForSeconds(2.5f);
        Instantiate(BossPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    private IEnumerator FadeInVolume()
    {
        bossMusic.Play();
        float elapsed = 0f;

        while (elapsed < fadeDurationIn)
        {
            elapsed += Time.deltaTime;
            bossMusic.volume = Mathf.Lerp(0f, originalVolume, elapsed / fadeDurationIn);
            yield return null;
        }

        bossMusic.volume = originalVolume;
    }
}
