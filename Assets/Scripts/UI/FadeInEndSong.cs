using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInEndSong : MonoBehaviour
{
    [SerializeField] private AudioSource endMusic;
    private float fadeDurationIn = 1f;
    private float originalVolume;

    private void Awake()
    {
        originalVolume = endMusic.volume;
    }

    private void Start()
    {
        StartCoroutine(FadeInVolume());
    }

    private IEnumerator FadeInVolume()
    {
        endMusic.Play();
        float elapsed = 0f;

        while (elapsed < fadeDurationIn)
        {
            elapsed += Time.deltaTime;
            endMusic.volume = Mathf.Lerp(0f, originalVolume, elapsed / fadeDurationIn);
            yield return null;
        }

        endMusic.volume = originalVolume;
    }
}
