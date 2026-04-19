using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightSystem : MonoBehaviour
{
    public GameObject flashlightObject;

    bool hasFlashlight = false;
    bool flashlightOn = false;

    void Update()
    {
        if (!hasFlashlight) return;

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            ToggleFlashlight();
        }
    }

    public void PickFlashlight()
    {
        hasFlashlight = true;

        flashlightObject.SetActive(true);
        flashlightOn = true;
    }

    void ToggleFlashlight()
    {
        flashlightOn = !flashlightOn;
        flashlightObject.SetActive(flashlightOn);
    }
}
