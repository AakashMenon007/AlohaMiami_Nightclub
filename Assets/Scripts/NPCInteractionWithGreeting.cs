using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionWithGreeting : MonoBehaviour
{
    public Animator npcAnimator; // Animator for the NPC
    public string greetTrigger = "Greet"; // Trigger name for the greet animation
    public AudioSource greetingAudio; // AudioSource for the greeting sound
    public float lookAtSpeed = 5f; // Speed at which the NPC turns to face the player

    private Transform player; // Reference to the player
    private bool isPlayerInRange = false; // Tracks if the player is in range

    void Start()
    {
        // Find the player (ensure the Player is tagged as "Player")
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the Player GameObject is tagged as 'Player'.");
        }

        // Ensure the NPC starts in the Idle animation
        if (npcAnimator != null)
        {
            npcAnimator.Play("Idle");
        }
    }

    void Update()
    {
        if (isPlayerInRange && player != null)
        {
            // Smoothly rotate to face the player
            LookAtPlayer();
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookAtSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;

            // Transition to greet animation
            if (npcAnimator != null)
            {
                npcAnimator.SetTrigger(greetTrigger);
            }

            // Play greeting audio
            if (greetingAudio != null && !greetingAudio.isPlaying)
            {
                greetingAudio.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

            // Return to idle animation
            if (npcAnimator != null)
            {
                npcAnimator.ResetTrigger(greetTrigger); // Clear the greet trigger
                npcAnimator.Play("Idle"); // Explicitly set the animation back to Idle
            }
        }
    }
}
