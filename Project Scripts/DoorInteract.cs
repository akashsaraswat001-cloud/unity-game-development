using UnityEngine;
using System.Collections;

public class DoorInteract : Interactable
{
    [Header("Lock Settings")]
    public bool isLocked = true;
    public string requiredKeyID;

    [Header("Door Settings")]
    bool isOpen = false;
    bool isMoving = false;

    public float openAngle = 90f;
    public float speed = 2f;

    PlayerCarrySystem playerCarry;

    [Header("Sound Settings")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip lockedSound;

    void Start()
    {
        playerCarry = FindObjectOfType<PlayerCarrySystem>();

        //  AUTO SET AUDIO SOURCE (NO NEED TO DRAG)
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 3D sound
    }

    public override void Interact()
    {
        if (isMoving) return;

        //  LOCK SYSTEM
        if (isLocked)
        {
            if (playerCarry == null || playerCarry.carriedObject == null)
            {
                PlaySound(lockedSound);
                return;
            }

            KeyItem key = playerCarry.carriedObject.GetComponent<KeyItem>();

            if (key != null && key.keyID == requiredKeyID && !key.isUsed)
            {
                key.isUsed = true;
                isLocked = false;

                playerCarry.DropObject();
            }
            else
            {
                PlaySound(lockedSound);
                return;
            }
        }

        StartCoroutine(SmoothDoor());
    }

    IEnumerator SmoothDoor()
    {
        isMoving = true;

        float direction = isOpen ? -openAngle : openAngle;

        //  PLAY SOUND
        if (!isOpen)
            PlaySound(openSound);
        else
            PlaySound(closeSound);

        Quaternion startRot = transform.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0, direction, 0);

        float t = 0;

        while (t < 1)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            t += Time.deltaTime * speed;
            yield return null;
        }

        transform.rotation = endRot;

        isOpen = !isOpen;
        isMoving = false;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(clip);
        }
    }

    public override string GetInteractionText()
    {
        return "";
    }
}
