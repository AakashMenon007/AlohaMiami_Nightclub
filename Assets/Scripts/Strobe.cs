using UnityEngine;

public class StrobingColorSpotlight : MonoBehaviour
{
    public Light spotlight; // Assign the Spotlight in the Inspector
    public Color[] colors; // Array of colors for the strobe effect
    public float strobeSpeed = 0.5f; // Time between strobe changes
    public float minIntensity = 0.0f; // Minimum intensity of the strobe
    public float maxIntensity = 5.0f; // Maximum intensity of the strobe

    private int currentColorIndex = 0; // Track the current color
    private float timer = 0f; // Timer for strobe effect

    void Start()
    {
        if (spotlight == null)
        {
            spotlight = GetComponent<Light>();
        }

        if (colors.Length == 0)
        {
            // Add default colors if none are assigned
            colors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow };
        }

        spotlight.color = colors[currentColorIndex];
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= strobeSpeed)
        {
            // Toggle intensity for strobe effect
            spotlight.intensity = spotlight.intensity == maxIntensity ? minIntensity : maxIntensity;

            // Change to the next color when intensity is at max
            if (spotlight.intensity == maxIntensity)
            {
                currentColorIndex = (currentColorIndex + 1) % colors.Length;
                spotlight.color = colors[currentColorIndex];
            }

            timer = 0f; // Reset timer
        }
    }
}
