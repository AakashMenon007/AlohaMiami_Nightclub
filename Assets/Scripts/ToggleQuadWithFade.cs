using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleQuadWithFade : MonoBehaviour
{
    public GameObject quad; // The quad to toggle
    public Toggle toggle;   // The toggle UI element
    public float fadeDuration = 1f; // Duration of the fade effect
    private Material quadMaterial;  // Material of the quad
    private Coroutine fadeCoroutine; // To ensure only one fade runs at a time

    void Start()
    {
        // Ensure the toggle and quad are assigned
        if (quad == null || toggle == null)
        {
            Debug.LogError("Quad or Toggle is not assigned!");
            return;
        }

        // Get the quad's material
        quadMaterial = quad.GetComponent<Renderer>().material;
        if (quadMaterial == null)
        {
            Debug.LogError("Quad does not have a material assigned!");
            return;
        }

        // Start with the quad turned off
        SetQuadAlpha(0);
        quad.SetActive(false);

        // Add listener to the toggle
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        if (isOn)
        {
            quad.SetActive(true); // Enable the quad for the fade-in
            fadeCoroutine = StartCoroutine(FadeQuad(0, 1)); // Fade in
        }
        else
        {
            fadeCoroutine = StartCoroutine(FadeQuad(1, 0)); // Fade out
        }
    }

    private IEnumerator FadeQuad(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = quadMaterial.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = alpha;
            quadMaterial.color = color;
            yield return null;
        }

        color.a = endAlpha;
        quadMaterial.color = color;

        // If fading out, turn off the quad after the fade
        if (endAlpha == 0)
        {
            quad.SetActive(false);
        }
    }

    private void SetQuadAlpha(float alpha)
    {
        Color color = quadMaterial.color;
        color.a = alpha;
        quadMaterial.color = color;
    }
}
