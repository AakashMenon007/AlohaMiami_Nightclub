using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public float globalVolume = 1.0f; // The global volume level to apply to all audio sources

    void Start()
    {
        // Get all children of this GameObject
        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        // Iterate over each child and set the volume
        foreach (Transform child in children)
        {
            AudioSource audioSource = child.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = globalVolume;
                Debug.Log($"Set volume of {audioSource.name} to {globalVolume}");
            }
            else
            {
                Debug.LogWarning($"No AudioSource component found on {child.name}");
            }
        }
    }
}
