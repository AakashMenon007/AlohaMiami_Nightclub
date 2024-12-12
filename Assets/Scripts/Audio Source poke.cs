using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudioSourcesOnPoke : MonoBehaviour
{
    public AudioSource[] audioSources; // Array of audio sources to toggle
    private bool isPlaying = false;    // Tracks whether the audio sources are playing

    public void OnPokePressed()
    {
        if (audioSources.Length == 0)
        {
            Debug.LogWarning("No audio sources assigned to toggle!");
            return;
        }

        // Toggle audio sources
        if (isPlaying)
        {
            StopAllAudio();
        }
        else
        {
            PlayAllAudio();
        }

        // Flip the state
        isPlaying = !isPlaying;
    }

    private void PlayAllAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void StopAllAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
