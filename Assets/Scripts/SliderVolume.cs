using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControllerWithSlider : MonoBehaviour
{
    public Slider volumeSlider; // The UI slider for volume control
    public List<AudioSource> audioSources; // List of audio sources to control

    void Start()
    {
        if (volumeSlider == null)
        {
            Debug.LogError("VolumeSlider is not assigned.");
            return;
        }

        if (audioSources.Count == 0)
        {
            Debug.LogWarning("No audio sources assigned.");
            return;
        }

        // Initialize the slider
        volumeSlider.minValue = 0f; // Minimum volume
        volumeSlider.maxValue = 1f; // Maximum volume
        volumeSlider.onValueChanged.AddListener(UpdateVolume); // Add listener for slider value changes
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
}
