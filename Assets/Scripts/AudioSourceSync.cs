using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceSync : MonoBehaviour
{
    public AudioSource[] audioSources; // Array of audio sources to sync
    public AudioClip clipToPlay; // Audio clip to play on all sources

    public void PlayAllAudio()
    {
        if (audioSources == null || audioSources.Length == 0)
        {
            Debug.LogWarning("No AudioSources assigned!");
            return;
        }

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null)
            {
                audioSource.clip = clipToPlay; // Assign the clip
                audioSource.PlayDelayed(0.01f); // Small delay to ensure synchronization
            }
        }
    }

    public void StopAllAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    public void PauseAllAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    public void ResumeAllAudio()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.UnPause();
            }
        }
    }
}
