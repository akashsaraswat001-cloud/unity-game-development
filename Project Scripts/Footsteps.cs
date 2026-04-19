using UnityEngine;
using UnityEngine.InputSystem;

public class FootstepSystem : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] footstepSounds; // Put 2 sounds here

    [Header("Settings")]
    public float stepDelay = 0.5f;
    public float walkVolume = 0.6f;

    private float stepTimer;
    private int currentIndex = 0; //  This controls 1→2→1→2

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
    }

    void Update()
    {
        if (IsMoving())
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepDelay;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    bool IsMoving()
    {
        if (Keyboard.current == null) return false;

        return Keyboard.current.wKey.isPressed ||
               Keyboard.current.aKey.isPressed ||
               Keyboard.current.sKey.isPressed ||
               Keyboard.current.dKey.isPressed;
    }

    void PlayFootstep()
    {
        if (footstepSounds == null || footstepSounds.Length == 0)
        {
            Debug.LogWarning("No footstep sounds assigned!");
            return;
        }

        //  PLAY CURRENT SOUND (1 then 2 then 1 then 2)
        audioSource.pitch = Random.Range(0.98f, 1.02f); // small realism
        audioSource.PlayOneShot(footstepSounds[currentIndex], walkVolume);

        //  SWITCH INDEX (toggle between 0 and 1)
        currentIndex++;

        if (currentIndex >= footstepSounds.Length)
        {
            currentIndex = 0;
        }
    }
}
