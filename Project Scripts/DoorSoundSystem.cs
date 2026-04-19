using UnityEngine;

public class DoorSoundSystem : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Door Sounds")]
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip lockedSound;

    void Start()
    {
        // Auto setup
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 3D sound
    }

    // 🔊 PLAY FUNCTIONS

    public void PlayOpen()
    {
        PlaySound(openSound);
    }

    public void PlayClose()
    {
        PlaySound(closeSound);
    }

    public void PlayLocked()
    {
        PlaySound(lockedSound);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.pitch = Random.Range(0.95f, 1.05f); // realism
        audioSource.PlayOneShot(clip);
    }
}
