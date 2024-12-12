using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Assign the AudioSource component of the bartender
    public OVRLipSyncContext lipSyncContext; // Assign the LipSyncContext component

    public void PlayAudioClip(string clipName)
    {
        if (audioSource == null || lipSyncContext == null)
        {
            Debug.LogWarning("AudioSource or LipSyncContext is not assigned.");
            return;
        }

        // Load the audio clip from Resources/Audio
        AudioClip clip = Resources.Load<AudioClip>($"Audio/{clipName}");
        if (clip != null)
        {
            audioSource.Stop(); // Stop any currently playing audio
            audioSource.clip = clip; // Assign the new clip
            audioSource.Play(); // Play the clip

            // Assign the AudioSource to the Lip Sync context
            lipSyncContext.audioSource = audioSource;

            Debug.Log($"Playing audio clip: {clipName}");
        }
        else
        {
            Debug.LogWarning($"Audio clip '{clipName}' not found in Resources/Audio.");
        }
    }
}
