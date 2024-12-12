using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudioOnToggle : MonoBehaviour
{
    public AudioSource[] audioSources; // The array of audio sources to play
    public Toggle toggle; // The toggle UI element

    void Start()
    {
        // Ensure the toggle and audio sources are assigned
        if (toggle == null || audioSources.Length == 0)
        {
            Debug.LogError("Toggle or audio sources are not assigned!");
            return;
        }

        // Add listener to the toggle
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null)
            {
                if (isOn)
                {
                    audioSource.Play(); // Play the audio when the toggle is turned on
                }
                else
                {
                    audioSource.Stop(); // Stop the audio when the toggle is turned off
                }
            }
        }
    }
}
