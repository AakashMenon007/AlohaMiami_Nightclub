using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPC3D : MonoBehaviour
{
    [Header("Character")]
    public string characterName = "";

    [Header("Yarn Specific")]
    public string talkToNode = "";
    public YarnProject scriptToLoad;
    public DialogueRunner dialogueRunner; // Reference to the dialogue control
    public GameObject dialogueCanvas;    // Reference to the canvas

    [Header("Dialogue Canvas")]
    public bool useAutomaticPlacement = true; // Toggle for manual or automatic canvas placement
    public Vector3 automaticOffset = new Vector3(0f, 2.0f, 0.0f); // Default automatic placement offset
    public Transform manualPlacementPoint;    // Manual placement point (set in Inspector)
    private float canvasTurnSpeed = 2;
    private bool canvasActive;
    private GameObject playerGameObject;

    void Start()
    {
        dialogueCanvas = GameObject.FindGameObjectWithTag("Dialogue Canvas");
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");

        if (scriptToLoad == null)
        {
            Debug.LogError("NPC3D not set up with yarn scriptToLoad", this);
        }

        if (string.IsNullOrEmpty(characterName))
        {
            Debug.LogWarning("NPC3D not set up with characterName", this);
        }

        if (string.IsNullOrEmpty(talkToNode))
        {
            Debug.LogError("NPC3D not set up with talkToNode", this);
        }

        if (dialogueRunner == null)
        {
            Debug.LogError("dialogueRunner not set up", this);
        }

        if (dialogueCanvas == null)
        {
            Debug.LogError("Dialogue Canvas not set up", this);
        }

        if (playerGameObject == null)
        {
            Debug.LogError("Player Game Object not set up", this);
        }

        if (scriptToLoad != null && dialogueRunner != null)
        {
            dialogueRunner.yarnProject = scriptToLoad; // Adds the script to the dialogue system
        }
    }

    void Update()
    {
        if (canvasActive)
        {
            Vector3 lookDir = dialogueCanvas.transform.position - playerGameObject.transform.position;
            float radians = Mathf.Atan2(lookDir.x, lookDir.z);
            float degrees = radians * Mathf.Rad2Deg;

            float str = Mathf.Min(canvasTurnSpeed * Time.deltaTime, 1);
            Quaternion targetRotation = Quaternion.Euler(0, degrees, 0);
            dialogueCanvas.transform.rotation = Quaternion.Slerp(dialogueCanvas.transform.rotation, targetRotation, str);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If other is player
        if (other.gameObject.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(talkToNode))
            {
                if (dialogueCanvas != null)
                {
                    canvasActive = true;

                    // Check whether to use automatic or manual placement
                    if (useAutomaticPlacement)
                    {
                        dialogueCanvas.transform.SetParent(transform); // Use the root to prevent scaling
                        dialogueCanvas.GetComponent<RectTransform>().anchoredPosition3D = transform.TransformVector(automaticOffset);
                    }
                    else if (manualPlacementPoint != null)
                    {
                        dialogueCanvas.transform.SetParent(manualPlacementPoint); // Parent to manual placement point
                        dialogueCanvas.transform.localPosition = Vector3.zero; // Ensure proper placement
                    }
                    else
                    {
                        Debug.LogWarning("Manual placement is enabled, but no manual placement point is assigned.");
                    }
                }

                if (dialogueRunner.IsDialogueRunning)
                {
                    dialogueRunner.Stop();
                }

                Debug.Log("Start dialogue");
                dialogueRunner.StartDialogue(talkToNode);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvasActive = false;
            dialogueRunner.Stop();
        }
    }
}
