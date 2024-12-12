using System.Collections;
using UnityEngine;

public class MultiAudioZoneControl : MonoBehaviour
{
    public AudioSource[] audioSourcesToControl; // Array of audio sources for this zone
    public float fadeDuration = 2f;            // Duration for fade in/out
    private bool isFading = false;             // Flag to prevent simultaneous fades

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            Debug.Log("Player entered trigger zone. Turning on audio sources in sync.");
            StartCoroutine(FadeInAudio());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            Debug.Log("Player exited trigger zone. Turning off audio sources in sync.");
            StartCoroutine(FadeOutAudio());
        }
    }

    private IEnumerator FadeOutAudio()
    {
        isFading = true;

        foreach (AudioSource source in audioSourcesToControl)
        {
            if (source == null) continue; // Skip null references
            Debug.Log($"Fading out audio source: {source.name}");

            float startVolume = source.volume;

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                source.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
                yield return null;
            }

            source.volume = 0;
            source.Stop(); // Stop playback after fade-out
            Debug.Log($"Audio source {source.name} stopped.");
        }

        isFading = false;
    }

    private IEnumerator FadeInAudio()
    {
        isFading = true;

        if (audioSourcesToControl.Length > 0 && audioSourcesToControl[0] != null)
        {
            // Sync all audio sources to the same playback time as the first audio source
            float startTime = audioSourcesToControl[0].time;

            foreach (AudioSource source in audioSourcesToControl)
            {
                if (source == null) continue; // Skip null references
                source.time = startTime;     // Synchronize playback time
                source.Play();               // Start playback
                Debug.Log($"Synchronized playback of audio source: {source.name}");
            }
        }

        foreach (AudioSource source in audioSourcesToControl)
        {
            if (source == null) continue; // Skip null references

            Debug.Log($"Fading in audio source: {source.name}");
            float targetVolume = 0.05f; // Set volume to 0.25

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                source.volume = Mathf.Lerp(0, targetVolume, t / fadeDuration);
                yield return null;
            }

            source.volume = targetVolume; // Ensure volume is at max
            Debug.Log($"Audio source {source.name} fully faded in to 0.05 volume.");
        }

        isFading = false;
    }
}
