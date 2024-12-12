using UnityEngine;
using UnityEngine.UI;

public class MultiAudioScratchingSlider : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource[] audioSources; // Array of audio sources to control

    [Header("UI Elements")]
    public Slider scratchingSlider; // The slider for scratching back and forth

    private bool isDragging = false; // Flag to check if the user is dragging the slider

    void Start()
    {
        if (audioSources.Length == 0)
        {
            Debug.LogError("No AudioSources assigned!");
            return;
        }

        if (scratchingSlider == null)
        {
            Debug.LogError("ScratchingSlider is not assigned!");
            return;
        }

        // Assume all audio sources have the same clip length
        AudioClip firstClip = audioSources[0].clip;
        if (firstClip == null)
        {
            Debug.LogError("No audio clip found on the first audio source!");
            return;
        }

        // Set up the slider values based on the audio clip length
        scratchingSlider.minValue = 0;
        scratchingSlider.maxValue = firstClip.length;
        scratchingSlider.onValueChanged.AddListener(OnSliderValueChanged);

        // Sync the slider's initial value with the first audio source's time
        scratchingSlider.value = audioSources[0].time;
    }

    void Update()
    {
        // If the user is not dragging the slider, sync its value with the first audio source's time
        if (!isDragging && audioSources.Length > 0 && audioSources[0].isPlaying)
        {
            scratchingSlider.value = audioSources[0].time;
        }
    }

    // Called when the slider value changes
    public void OnSliderValueChanged(float value)
    {
        if (isDragging)
        {
            foreach (var source in audioSources)
            {
                if (source != null && source.clip != null)
                {
                    source.time = value; // Set the playback position for each audio source
                }
            }
        }
    }

    // Called when the user starts dragging the slider
    public void OnSliderDragStart()
    {
        isDragging = true;

        // Pause all audio sources while dragging for smooth scratching
        foreach (var source in audioSources)
        {
            if (source != null)
            {
                source.Pause();
            }
        }
    }

    // Called when the user stops dragging the slider
    public void OnSliderDragEnd()
    {
        isDragging = false;

        // Resume playing all audio sources after dragging
        foreach (var source in audioSources)
        {
            if (source != null)
            {
                source.Play();
            }
        }
    }
}
