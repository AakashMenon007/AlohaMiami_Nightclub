using UnityEngine;

public class CharacterHeadRotation : MonoBehaviour
{
    [Header("Head Rotation Settings")]
    public Transform headTransform; // Reference to the head's transform
    public Transform playerTransform; // Reference to the player's transform

    [Header("Rotation Constraints")]
    [Range(0f, 360f)]
    public float maxHorizontalRotation = 90f; // Maximum horizontal head rotation
    [Range(0f, 360f)]
    public float maxVerticalRotation = 60f; // Maximum vertical head rotation

    [Header("Rotation Smoothing")]
    public float rotationSpeed = 5f; // Smoothness of head rotation
    public bool enableSmoothing = true; // Toggle smooth rotation

    [Header("Debug")]
    public bool showDebugRay = false; // Visualize the look direction in scene view

    private void Start()
    {
        // If player transform is not manually set, try to find it
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Validate head transform
        if (headTransform == null)
        {
            Debug.LogWarning("Head transform is not assigned. Please assign in the inspector.");
        }

        if (playerTransform == null)
        {
            Debug.LogError("No player transform found. Ensure player has 'Player' tag.");
        }
    }

    private void Update()
    {
        if (headTransform == null || playerTransform == null)
            return;

        // Calculate direction to player
        Vector3 directionToPlayer = playerTransform.position - headTransform.position;
        directionToPlayer.y = 0; // Ignore vertical difference for initial horizontal rotation

        // Calculate look rotation
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Get local euler angles to check rotation limits
        Vector3 localEulerAngles = transform.InverseTransformDirection(targetRotation.eulerAngles);

        // Clamp horizontal and vertical rotations
        float clampedHorizontal = Mathf.Clamp(localEulerAngles.y, -maxHorizontalRotation, maxHorizontalRotation);
        float clampedVertical = Mathf.Clamp(localEulerAngles.x, -maxVerticalRotation, maxVerticalRotation);

        // Reconstruct rotation with clamped values
        Quaternion clampedRotation = Quaternion.Euler(
            clampedVertical,
            clampedHorizontal,
            0
        );

        // Apply rotation (smooth or instant)
        if (enableSmoothing)
        {
            headTransform.rotation = Quaternion.Slerp(
                headTransform.rotation,
                clampedRotation,
                rotationSpeed * Time.deltaTime
            );
        }
        else
        {
            headTransform.rotation = clampedRotation;
        }

        // Debug visualization
        if (showDebugRay)
        {
            Debug.DrawRay(headTransform.position, directionToPlayer, Color.red);
        }
    }
}