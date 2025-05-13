using UnityEngine;
using UnityEngine.InputSystem;

public class JewelInteraction : MonoBehaviour
{
    [Header("Config")]
    public GameObject eCanvas;
    public float interactionDistance = 4f;
    public GameObject dragonObject;

    [Header("Animations")]
    public float floatSpeed = 1f;
    public float floatHeight = 0.2f;

    private Transform player;
    private Vector3 initialCanvasPos;
    private bool playerInRange;
    private bool hasInteracted = false;

    void Start()
    {
        player = Camera.main.transform;
        initialCanvasPos = eCanvas.transform.localPosition;
        eCanvas.SetActive(false);
    }

    void Update()
    {
        if (hasInteracted) return;

        float distance = Vector3.Distance(player.position, transform.position);
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
        else
        {
            if (eCanvas.activeSelf)
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

        if (dragonObject != null)
            dragonObject.SetActive(true);

        eCanvas.SetActive(false);
        hasInteracted = true;
        gameObject.SetActive(false);
    }
}
