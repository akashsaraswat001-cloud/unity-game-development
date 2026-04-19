using UnityEngine;

public class ItemSound : MonoBehaviour
{
    public AudioClip pickupSound;
    public AudioClip dropSound;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 3D sound
    }

    public void PlayPickup()
    {
        if (pickupSound != null)
            audioSource.PlayOneShot(pickupSound);
    }

    public void PlayDrop()
    {
        if (dropSound != null)
            audioSource.PlayOneShot(dropSound);
    }
}
