using UnityEngine;

public class DialogueCanvasFollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float heightOffset = 1.5f; // Height above the player's position
    public float distanceOffset = 1f; // Distance in front of the player
    public float followSpeed = 5f; // Smooth following speed

    private void Update()
    {
        if (player != null)
        {
            // Calculate target position in front of the player
            Vector3 targetPosition = player.position + player.forward * distanceOffset;
            targetPosition.y += heightOffset;

            // Smoothly move the canvas to the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Make the canvas face the player
            transform.LookAt(player);
            transform.Rotate(0, 180, 0); // Invert the rotation to face the player correctly
        }
    }
}
