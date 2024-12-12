using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReverbControllerWithSlider_NoMixer : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider reverbSlider; // The slider to control reverb

    [Header("Audio Sources")]
    public List<AudioSource> audioSources; // List of audio sources to control

    private List<AudioReverbFilter> reverbFilters = new List<AudioReverbFilter>(); // List of reverb filters

    void Start()
    {
        if (reverbSlider == null)
        {
            Debug.LogError("ReverbSlider is not assigned.");
            return;
        }

        if (audioSources.Count == 0)
        {
            Debug.LogWarning("No audio sources assigned.");
            return;
        }

        // Attach or retrieve AudioReverbFilters for each audio source
        foreach (var audioSource in audioSources)
        {
            if (audioSource != null)
            {
                var reverbFilter = audioSource.gameObject.GetComponent<AudioReverbFilter>();
                if (reverbFilter == null)
                {
                    reverbFilter = audioSource.gameObject.AddComponent<AudioReverbFilter>();
                }

                reverbFilters.Add(reverbFilter);
            }
            else
            {
                Debug.LogWarning("Audio source is not assigned or is null.");
            }
        }

        // Initialize the slider
        reverbSlider.minValue = -10000f; // Minimum reverb level (fully muted)
        reverbSlider.maxValue = 0f;     // Maximum reverb level (default reverb)
        reverbSlider.onValueChanged.AddListener(UpdateReverb); // Add listener for slider value changes
    }

    private void UpdateReverb(float value)
    {
        foreach (var reverbFilter in reverbFilters)
        {
            if (reverbFilter != null)
            {
                reverbFilter.reverbLevel = value; // Set the reverb level for each filter
                Debug.Log($"Updated reverb level of {reverbFilter.gameObject.name} to {value} dB");
            }
        }
    }
}
