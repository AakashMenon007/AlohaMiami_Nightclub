using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;

public class XRHandMovement : MonoBehaviour
{
    public XRHandSubsystem handSubsystem; // The XR Hand Subsystem
    public Transform cameraTransform; // Reference to the camera (for forward movement direction)
    public float moveSpeed = 2f; // Movement speed
    public float rotationSpeed = 100f; // Rotation speed

    private XRHand leftHand; // Reference to the left hand
    private XRHand rightHand; // Reference to the right hand

    void Start()
    {
        // Ensure the hand subsystem is initialized
        if (handSubsystem == null)
        {
            handSubsystem = XRGeneralSettings.Instance.Manager.activeLoader.GetLoadedSubsystem<XRHandSubsystem>();
            if (handSubsystem == null)
            {
                Debug.LogError("XR Hand Subsystem is not initialized!");
            }
        }
    }

    void Update()
    {
        if (handSubsystem == null) return;

        // Update hand references
        leftHand = handSubsystem.leftHand;
        rightHand = handSubsystem.rightHand;

        // Check if hands are valid
        if (leftHand.isTracked && rightHand.isTracked)
        {
            HandleMovement(leftHand);
            HandleRotation(rightHand);
        }
    }

    private void HandleMovement(XRHand hand)
    {
        // Use the palm's normal vector for movement (e.g., palm facing forward to move forward)
        XRHandJoint palmJoint = hand.GetJoint(XRHandJointID.Palm);
        if (palmJoint.TryGetPose(out Pose palmPose))
        {
            Vector3 moveDirection = palmPose.forward; // Forward direction of the palm
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation(XRHand hand)
    {
        // Use hand rotation gestures for player rotation
        XRHandJoint wristJoint = hand.GetJoint(XRHandJointID.Wrist);
        if (wristJoint.TryGetPose(out Pose wristPose))
        {
            // Rotate the player based on wrist yaw movement
            float rotationInput = wristPose.rotation.eulerAngles.y;
            transform.Rotate(0, rotationInput * rotationSpeed * Time.deltaTime, 0);
        }
    }
}
