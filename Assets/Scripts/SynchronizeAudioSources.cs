using UnityEngine;

public class AudioSourceSynchronizer : MonoBehaviour
{
    public AudioSource masterAudioSource; // The main audio source to play
    public AudioSource[] linkedAudioSources; // Array of linked audio sources to synchronize

    void Start()
    {
        if (masterAudioSource == null || linkedAudioSources.Length == 0)
        {
            Debug.LogError("Master Audio Source or linked audio sources are not assigned!");
            return;
        }

        // Play the master audio source and link the audio
        masterAudioSource.Play();
        SynchronizeAudioSources();
    }

    void Update()
    {
        // Check if the master audio source is playing
        if (masterAudioSource.isPlaying)
        {
            // Ensure all linked audio sources play the same
            SynchronizeAudioSources();
        }
    }

    private void SynchronizeAudioSources()
    {
        foreach (AudioSource audioSource in linkedAudioSources)
        {
            if (audioSource != null && audioSource.clip == null)
            {
                audioSource.clip = masterAudioSource.clip; // Assign the clip from the master audio source
            }

            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.volume = masterAudioSource.volume; // Synchronize volume
                audioSource.mute = masterAudioSource.mute; // Synchronize mute
                audioSource.pitch = masterAudioSource.pitch; // Synchronize pitch
                audioSource.time = masterAudioSource.time; // Synchronize playback position
                audioSource.Play(); // Play the synchronized audio
            }
        }
    }
}
