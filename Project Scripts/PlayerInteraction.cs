using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    [Header("UI")]
    public TextMeshProUGUI interactionTextUI;

    [Header("Reticle Settings")]
    public RectTransform reticle;
    public float normalSize = 8f;
    public float interactSize = 16f;
    public float sizeSpeed = 10f;

    private Camera cam;
    private Interactable currentInteractable;

    void Start()
    {
        cam = Camera.main;

        if (interactionTextUI != null)
            interactionTextUI.text = "";
    }

    void Update()
    {
        HandleRaycast();
        HandleInput();
    }

    void HandleRaycast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            Interactable interactable = hit.collider.GetComponentInParent<Interactable>();

            if (interactable != null)
            {
                currentInteractable = interactable;

                // UI TEXT
                if (interactionTextUI != null)
                    interactionTextUI.text = interactable.GetInteractionText();

                SetReticleSize(interactSize);
                return;
            }
        }

        // Nothing hit
        currentInteractable = null;

        if (interactionTextUI != null)
            interactionTextUI.text = "";

        SetReticleSize(normalSize);
    }

    void HandleInput()
    {
        if (currentInteractable != null &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentInteractable.Interact();
        }
    }

    void SetReticleSize(float targetSize)
    {
        if (reticle == null) return;

        Vector2 target = new Vector2(targetSize, targetSize);

        reticle.sizeDelta = Vector2.Lerp(
            reticle.sizeDelta,
            target,
            Time.deltaTime * sizeSpeed
        );
    }
}
