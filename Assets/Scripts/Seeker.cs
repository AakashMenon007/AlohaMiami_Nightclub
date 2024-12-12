using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiAudioSeeker : MonoBehaviour
{
    public AudioSource[] audioSources; // Array of audio sources
    public Slider seekerSlider;        // Slider to control playback position
    public Text currentTimeText;       // Text to display current playback time (optional)
    public Text durationText;          // Text to display total duration of the audio (optional)

    private AudioSource currentAudioSource; // The currently active audio source
    private bool isDragging = false;        // To track if the user is dragging the slider

    void Start()
    {
        if (audioSources.Length == 0 || seekerSlider == null)
        {
            Debug.LogError("Please assign at least one AudioSource and the Seeker Slider in the Inspector!");
            return;
        }

        // Set the first audio source as the current one
        SetActiveAudioSource(audioSources[0]);

        // Initialize seeker slider
        seekerSlider.minValue = 0;
        seekerSlider.maxValue = 1; // Normalized playback position
        seekerSlider.onValueChanged.AddListener(OnSeekerSliderChanged);
    }

    void Update()
    {
        if (currentAudioSource == null || currentAudioSource.clip == null) return;

        // Update current time and seeker slider if not dragging
        if (!isDragging)
        {
            seekerSlider.value = currentAudioSource.time / currentAudioSource.clip.length;
        }

        // Update current time text
        if (currentTimeText != null)
        {
            currentTimeText.text = FormatTime(currentAudioSource.time);
        }
    }

    public void SetActiveAudioSource(AudioSource newAudioSource)
    {
        if (newAudioSource == null || newAudioSource == currentAudioSource) return;

        // Stop the current audio source
        if (currentAudioSource != null && currentAudioSource.isPlaying)
        {
            currentAudioSource.Stop();
        }

        // Set the new audio source
        currentAudioSource = newAudioSource;

        // Update duration text
        if (durationText != null && currentAudioSource.clip != null)
        {
            durationText.text = FormatTime(currentAudioSource.clip.length);
        }

        // Reset seeker slider
        seekerSlider.value = 0;

        // Start playing the new audio source
        currentAudioSource.Play();
    }

    private void OnSeekerSliderChanged(float value)
    {
        if (currentAudioSource != null && currentAudioSource.clip != null && isDragging)
        {
            // Seek to the new time
            currentAudioSource.time = value * currentAudioSource.clip.length;
        }
    }

    public void OnSeekerDragStart()
    {
        // Called when the user starts dragging the seeker slider
        isDragging = true;
    }

    public void OnSeekerDragEnd()
    {
        // Called when the user releases the seeker slider
        isDragging = false;
    }

    private string FormatTime(float timeInSeconds)
    {
        // Format time as MM:SS
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
