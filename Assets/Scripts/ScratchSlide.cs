using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratchingEffectController : MonoBehaviour
{
    public Slider scratchSlider; // The UI slider for scratching control
    public List<AudioSource> audioSources; // List of audio sources to control scratching

    void Start()
    {
        if (scratchSlider == null)
        {
            Debug.LogError("ScratchSlider is not assigned.");
            return;
        }

        if (audioSources.Count == 0)
        {
            Debug.LogWarning("No audio sources assigned.");
            return;
        }

        // Initialize the scratch slider
        scratchSlider.minValue = 0f; // Start at the beginning
        scratchSlider.maxValue = 1f; // End at the end
        scratchSlider.onValueChanged.AddListener(UpdateScratching); // Add listener for scratching changes
    }

    private void UpdateScratching(float value)
    {
        foreach (var audioSource in audioSources)
        {
            if (audioSource != null && audioSource.clip != null)
            {
                // Calculate the playback position based on the slider value
                float newPosition = value * audioSource.clip.length;
                audioSource.time = newPosition; // Set the new playback position
                Debug.Log($"Updated scratch position of {audioSource.name} to {newPosition}");
            }
            else
            {
                Debug.LogWarning("Audio source is not assigned, is null, or does not have an audio clip.");
            }
        }
    }
}
