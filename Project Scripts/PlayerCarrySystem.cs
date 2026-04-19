using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PlayerCarrySystem : MonoBehaviour
{
    public Transform carryPoint;
    public GameObject carriedObject;

    public float dropForce = 3f;

    [Header("UI Warning")]
    public TextMeshProUGUI warningText;
    public float warningTime = 2f;

    void Update()
    {
        if (carriedObject == null)
            return;

        if (Keyboard.current != null && Keyboard.current.gKey.wasPressedThisFrame)
        {
            DropObject();
        }
    }

    public void PickUpObject(GameObject obj)
    {
        //  Already carrying
        if (carriedObject != null)
        {
            StartCoroutine(ShowWarning("You can carry only one item"));
            return;
        }

        if (obj == null) return;

        carriedObject = obj;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Collider col = obj.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        //  SAFE PARENT (IMPORTANT FIX)
        obj.transform.SetParent(carryPoint, true); // <-- THIS FIXES SCALE BUG

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        //  Pickup sound
        ItemSound sound = obj.GetComponent<ItemSound>();
        if (sound != null && sound.pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(sound.pickupSound, transform.position);
        }
    }

    public void DropObject()
    {
        if (carriedObject == null)
            return;

        GameObject obj = carriedObject;

        Vector3 dropPosition = carryPoint.position + carryPoint.forward * 0.8f;

        obj.transform.SetParent(null);
        obj.transform.position = dropPosition;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.AddForce(carryPoint.forward * dropForce, ForceMode.Impulse);
        }

        Collider col = obj.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }

        //  Drop sound
        ItemSound sound = obj.GetComponent<ItemSound>();
        if (sound != null && sound.dropSound != null)
        {
            AudioSource.PlayClipAtPoint(sound.dropSound, obj.transform.position);
        }

        carriedObject = null;
    }

    IEnumerator ShowWarning(string message)
    {
        if (warningText == null) yield break;

        warningText.text = message;
        warningText.gameObject.SetActive(true);

        yield return new WaitForSeconds(warningTime);

        warningText.gameObject.SetActive(false);
    }
}
