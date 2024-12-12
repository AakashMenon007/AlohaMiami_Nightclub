using UnityEngine;

public class PlayAudioOnceAndDeleteOnTrigger : MonoBehaviour
{
    public AudioSource audioSource; // The audio source to play

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the collider has the "Player" tag
        if (other.CompareTag("Player"))
        {
            if (audioSource != null)
            {
                audioSource.Play(); // Play the audio
                Destroy(audioSource.gameObject); // Destroy the audio source object after playback
            }
        }
    }
}
