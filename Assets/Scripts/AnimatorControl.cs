using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterExitZone : MonoBehaviour
{
    public List<GameObject> djObjects; // List of DJ GameObjects with Animator component

    private List<Animator> djAnimators;
    private static readonly int DJEntryTransition = Animator.StringToHash("DJEntry");
    private static readonly int DJExitTransition = Animator.StringToHash("DJExit");

    void Start()
    {
        if (djObjects == null || djObjects.Count == 0)
        {
            Debug.LogError("DJ GameObject list is not assigned or empty.");
            return;
        }

        djAnimators = new List<Animator>();

        // Populate the djAnimators list with Animator components from djObjects
        foreach (var djObject in djObjects)
        {
            if (djObject != null)
            {
                Animator animator = djObject.GetComponent<Animator>();
                if (animator != null)
                {
                    djAnimators.Add(animator);
                }
                else
                {
                    Debug.LogError($"Animator not found on DJ GameObject: {djObject.name}");
                }
            }
            else
            {
                Debug.LogWarning("Null DJ GameObject found in list.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            foreach (var animator in djAnimators)
            {
                // Set DJ entry transition
                animator.SetTrigger(DJEntryTransition);
                Debug.Log($"Player entered trigger zone. DJ Entry transition activated on {animator.gameObject.name}.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            foreach (var animator in djAnimators)
            {
                // Reset DJ entry trigger and set DJ exit transition
                animator.ResetTrigger(DJEntryTransition);
                animator.SetTrigger(DJExitTransition);
                Debug.Log($"Player exited trigger zone. DJ Exit transition activated on {animator.gameObject.name}.");
            }
        }
    }
}
