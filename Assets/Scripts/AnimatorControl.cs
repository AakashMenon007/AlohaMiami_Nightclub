using UnityEngine;

public class DJAnimatorController : MonoBehaviour
{
    public Animator djAnimator; // Reference to the Animator component

    private static readonly int DJEntryTrigger = Animator.StringToHash("DJEntry");
    private static readonly int DJExitTrigger = Animator.StringToHash("DJExit");

    void Start()
    {
        if (djAnimator == null)
        {
            Debug.LogError("DJ Animator is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Set DJ entry trigger
            djAnimator.SetTrigger(DJEntryTrigger);
            Debug.Log("DJ Entry Trigger activated.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Reset DJ entry trigger to default animation
            djAnimator.ResetTrigger(DJEntryTrigger);
            djAnimator.SetTrigger(DJExitTrigger);
            Debug.Log("DJ Exit Trigger activated.");
        }
    }
}
