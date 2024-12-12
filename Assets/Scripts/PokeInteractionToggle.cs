using UnityEngine;

public class XRPokeButtonLightToggle : MonoBehaviour
{
    public Light targetLight; // Reference to the point light to toggle

    private void Start()
    {
        // Ensure the light is initially off
        if (targetLight != null)
        {
            targetLight.enabled = false;
        }
        else
        {
            Debug.LogWarning("No Light assigned to the XR Poke Button.");
        }
    }

    public void OnPoke()
    {
        if (targetLight != null)
        {
            // Toggle the light's enabled state
            targetLight.enabled = !targetLight.enabled;

            // Log the light's current state for debugging
            Debug.Log($"Light is now {(targetLight.enabled ? "ON" : "OFF")}");
        }
        else
        {
            Debug.LogWarning("No Light assigned to the XR Poke Button.");
        }
    }
}
