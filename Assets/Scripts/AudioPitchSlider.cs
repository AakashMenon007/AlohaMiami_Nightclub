using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAndPitchController : MonoBehaviour
{
    public Slider volumeSlider; // The UI slider for volume control
    public Slider pitchSlider;  // The UI slider for pitch control
    public List<AudioSource> audioSources; // List of audio sources to control

    void Start()
    {
        if (volumeSlider == null)
        {
            Debug.LogError("VolumeSlider is not assigned.");
            return;
        }

        if (pitchSlider == null)
        {
            Debug.LogError("PitchSlider is not assigned.");
            return;
        }

        if (audioSources.Count == 0)
        {
            Debug.LogWarning("No audio sources assigned.");
            return;
        }

        // Initialize the volume slider
        volumeSlider.minValue = 0f; // Minimum volume (mute)
        volumeSlider.maxValue = 1f; // Maximum volume (full volume)
        volumeSlider.onValueChanged.AddListener(UpdateVolume); // Add listener for volume changes

        // Initialize the pitch slider
        pitchSlider.minValue = 0.5f; // Minimum pitch (half speed)
        pitchSlider.maxValue = 2f;   // Maximum pitch (double speed)
        pitchSlider.onValueChanged.AddListener(UpdatePitch); // Add listener for pitch changes
    }

    private void UpdateVolume(float value)
    {
        foreach (var audioSource in audioSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = value; // Set the volume of each audio source
                Debug.Log($"Updated volume of {audioSource.name} to {value}");
            }
            else
            {
                Debug.LogWarning("Audio source is not assigned or is null.");
            }
        }
    }

    private void UpdatePitch(float value)
    {
        foreach (var audioSource in audioSources)
        {
            if (audioSource != null)
            {
                audioSource.pitch = value; // Set the pitch of each audio source
                Debug.Log($"Updated pitch of {audioSource.name} to {value}");
            }
            else
            {
                Debug.LogWarning("Audio source is not assigned or is null.");
            }
        }
    }
}
