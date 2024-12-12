using UnityEngine;

public class MultiStrobingSpotlights : MonoBehaviour
{
    [System.Serializable]
    public class StrobeLightSettings
    {
        public Light spotLight; // The spotlight for this strobe effect
        public float minIntensity = 0f; // Minimum intensity of the light
        public float maxIntensity = 5f; // Maximum intensity of the light
        public float strobeSpeed = 10f; // Speed of the strobe effect
        public Color lightColor = Color.white; // Color of the light
    }

    [Header("Strobe Lights Configuration")]
    public StrobeLightSettings[] strobeLights; // Array to hold multiple lights with individual settings

    void Start()
    {
        foreach (var strobe in strobeLights)
        {
            // Ensure each light is valid and is a spotlight
            if (strobe.spotLight == null || strobe.spotLight.type != LightType.Spot)
            {
                Debug.LogError("Each light must be assigned and be of type Spotlight!");
                continue;
            }

            // Set the initial light color
            strobe.spotLight.color = strobe.lightColor;
        }
    }

    void Update()
    {
        foreach (var strobe in strobeLights)
        {
            if (strobe.spotLight == null) continue;

            // Adjust the light intensity for strobe effect
            float intensity = Mathf.PingPong(Time.time * strobe.strobeSpeed, strobe.maxIntensity - strobe.minIntensity) + strobe.minIntensity;
            strobe.spotLight.intensity = intensity;

            // Optional: Dynamically update color (e.g., to animate colors over time)
            // strobe.spotLight.color = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1f));
        }
    }
}

