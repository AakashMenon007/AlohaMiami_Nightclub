using UnityEngine;
using UnityEngine.UI;

public class PlaySelectedSong : MonoBehaviour
{
    public AudioSource[] audioSources; // Array of all song audio sources
    public Button[] songButtons; // Buttons representing each song
    public Text selectedSongDisplay; // Optional UI text to display the selected song's name

    private int selectedIndex = -1; // Index of the selected song in the audio sources array

    void Start()
    {
        // Set up buttons with their song selection functions
        for (int i = 0; i < songButtons.Length; i++)
        {
            int songIndex = i;
            songButtons[i].onClick.AddListener(() => SelectSong(songIndex));
        }
    }

    public void SelectSong(int songIndex)
    {
        // Select the audio source based on song index
        if (audioSources.Length > songIndex)
        {
            AudioSource selectedAudio = audioSources[songIndex];
            if (selectedAudio != null)
            {
                selectedAudio.Play(); // Play the selected song

                // Optional: Display the selected song name
                if (selectedSongDisplay != null)
                {
                    selectedSongDisplay.text = songButtons[songIndex].GetComponentInChildren<Text>().text;
                }

                selectedIndex = songIndex; // Update the selected index
            }
        }
    }

    public void OnScrollbarSelect()
    {
        // This function should be called when the scrollbar selects a new item
        // For example, via OnValueChanged event
        // Use the scrollbar's value to determine the selected index
        // Example: selectedIndex = Mathf.FloorToInt(scrollbar.value * (audioSources.Length - 1));
        // Update selected song
        SelectSong(selectedIndex);
    }
}
