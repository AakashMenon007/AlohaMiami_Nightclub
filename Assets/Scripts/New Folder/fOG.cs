using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogMachinePokeInteraction : MonoBehaviour
{
    public ParticleSystem fogMachine1; // First fog machine particle system
    public ParticleSystem fogMachine2; // Second fog machine particle system
    private bool isFogActive = false; // Tracks if the fog machines are active

    public void OnPokePressed()
    {
        isFogActive = !isFogActive; // Toggle the fog state

        if (isFogActive)
        {
            StartFog();
        }
        else
        {
            StopFog();
        }
    }

    private void StartFog()
    {
        if (fogMachine1 != null && fogMachine2 != null)
        {
            fogMachine1.Play();
            fogMachine2.Play();
        }
        else
        {
            Debug.LogWarning("Fog machines are not assigned in the Inspector!");
        }
    }

    private void StopFog()
    {
        if (fogMachine1 != null && fogMachine2 != null)
        {
            fogMachine1.Stop();
            fogMachine2.Stop();
        }
    }
}
