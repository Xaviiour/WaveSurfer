using UnityEngine;
using System.Collections;

public static class AudioFadeOut
{
    // call with StartCoroutine (AudioFadeOut.FadeOut (sound_open, 0.1f));
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

}
