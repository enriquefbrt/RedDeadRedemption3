using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class SecretScript : MonoBehaviour
{
    [SerializeField] private GameObject secretText;
    private AudioSource music;
    private Following cameraScript;
    private GameObject mainCamera;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private float duration = 3f;
    private Coroutine moveCameraCoroutine;

    void Start()
    {
        music = GetComponentInChildren<AudioSource>();
        cameraScript = FindAnyObjectByType<Following>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            music.Play();
            cameraScript.enabled = false;
            originalPosition = mainCamera.transform.position;
            targetPosition = new Vector3(originalPosition.x, originalPosition.y - 50, originalPosition.z - 180);
            moveCameraCoroutine = StartCoroutine(MoveCamera(mainCamera.transform.position, targetPosition, duration));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            music.Stop();
            StopCoroutine(moveCameraCoroutine);
            cameraScript.enabled = true;
        }
    }

    IEnumerator MoveCamera(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        yield return new WaitForSeconds(10f);
        secretText.SetActive(false);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = endPosition;
    }
}
