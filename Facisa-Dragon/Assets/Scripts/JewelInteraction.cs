using UnityEngine;
using UnityEngine.InputSystem;

public class JewelInteraction : MonoBehaviour
{
    [Header("References")]
    public GameObject eCanvas;
    public GameObject dragonObject;
    public GameObject swordObject;
    public GameObject playerSword;
    public StarterAssets.FirstPersonController playerController;
    public Transform playerTransform;
    public CameraShake cameraShake;
    public GameObject hudUI;

    [Header("Distance")]
    public float interactionDistance = 4f;

    [Header("Push")]
    public float pushBackSpeed = 5f;
    public float upwardSpeed = 8f;

    [Header("Shake")]
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 2f;

    [Header("Animation")]
    public float floatSpeed = 1f;
    public float floatHeight = 0.2f;

    private bool hasInteracted = false;
    private bool playerInRange;
    private Transform playerCamera;
    private Vector3 initialCanvasPos;

    void Start()
    {
        playerCamera = Camera.main.transform;
        initialCanvasPos = eCanvas.transform.localPosition;
        eCanvas.SetActive(false);

        if (playerSword != null)
            playerSword.SetActive(false);

        if (hudUI != null)
            hudUI.SetActive(false);    
    }

    void Update()
    {
        if (hasInteracted) return;

        float distance = Vector3.Distance(playerCamera.position, transform.position);
        playerInRange = distance <= interactionDistance;

        if (playerInRange)
        {
            if (!eCanvas.activeSelf)
                eCanvas.SetActive(true);

            AnimateCanvas();

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Interact();
            }
        }
        else if (eCanvas.activeSelf)
        {
            eCanvas.SetActive(false);
        }
    }

    void AnimateCanvas()
    {
        Vector3 newPos = initialCanvasPos;
        newPos.y += Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        eCanvas.transform.localPosition = newPos;
    }

    void Interact()
    {
        if (hasInteracted) return;
        hasInteracted = true;

        if (dragonObject != null)
            dragonObject.SetActive(true);

        if (swordObject != null)
            swordObject.SetActive(false);

        if (playerSword != null)
            playerSword.SetActive(true);

        if (hudUI != null)
            hudUI.SetActive(true);

        if (playerController != null && playerTransform != null)
        {
            Vector3 force = Vector3.up * upwardSpeed + (-playerTransform.forward * pushBackSpeed);
            playerController.ApplyExternalForce(force);
        }

        if (cameraShake != null)
            cameraShake.Shake(6f, 1f);


        if (eCanvas.activeSelf)
            eCanvas.SetActive(false);

        gameObject.SetActive(false);
    }
}
