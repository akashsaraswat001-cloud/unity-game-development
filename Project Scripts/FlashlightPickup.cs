using UnityEngine;

public class FlashlightPickup : Interactable
{
    FlashlightSystem flashlightSystem;

    void Start()
    {
        flashlightSystem = FindObjectOfType<FlashlightSystem>();
    }

    public override void Interact()
    {
        flashlightSystem.PickFlashlight();
        Destroy(gameObject);
    }
}
