using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeBossSilence : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    private float fadeDurationOut = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOutVolume());
        }
    }
    private IEnumerator FadeOutVolume()
    {
        float startVolume = music.volume;
        float elapsed = 0f;

        while (elapsed < fadeDurationOut)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDurationOut);
            music.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        music.volume = 0f;
        music.Stop();
    }
}
