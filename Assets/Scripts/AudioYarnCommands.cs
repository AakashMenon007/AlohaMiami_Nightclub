using UnityEngine;
using Yarn.Unity;

public class AudioYarnCommands : MonoBehaviour
{
    public AudioManager audioManager; // Reference to the AudioManager

    [YarnCommand("playAudio")]
    public void PlayAudio(string clipName)
    {
        if (audioManager != null)
        {
            audioManager.PlayAudioClip(clipName);
        }
        else
        {
            Debug.LogWarning("AudioManager is not assigned.");
        }
    }
}
