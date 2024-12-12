using UnityEngine;
using UnityEngine.UI;

public class TogglePrefabControl : MonoBehaviour
{
    public GameObject targetPrefab; // The prefab to control with the toggle
    public Toggle toggle;          // Reference to the Toggle UI component

    void Start()
    {
        if (toggle == null)
        {
            Debug.LogError("TogglePrefabControl requires a Toggle component attached.");
            return;
        }

        toggle.onValueChanged.AddListener(OnToggleChanged); // Add listener to the toggle
    }

    private void OnToggleChanged(bool isOn)
    {
        if (targetPrefab != null)
        {
            targetPrefab.SetActive(isOn); // Enable or disable the prefab based on toggle state
            Debug.Log($"{targetPrefab.name} turned {(isOn ? "on" : "off")}");
        }
        else
        {
            Debug.LogWarning("Target Prefab is not assigned.");
        }
    }
}
