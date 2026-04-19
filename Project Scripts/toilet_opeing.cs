using UnityEngine;
using UnityEngine.InputSystem;

public class ToiletSystem : Interactable
{
    public Transform upperLid;
    public Transform lowerSeat;

    public ParticleSystem peeParticles;
    public MonoBehaviour playerMovement;

    public GameObject peeUI; //  UI TEXT (Press G to Pee)

    public float speed = 3f;
    public float peeDuration = 5f;
    public float peeCooldown = 420f; // 7 min

    private float cooldownTimer = 0f;
    private bool canPee = true;

    private Quaternion upperOpenRot;
    private Quaternion upperClosedRot;
    private Quaternion lowerOpenRot;
    private Quaternion lowerClosedRot;

    private bool upperOpened = false;
    private bool lowerOpened = false;
    private bool isPeeing = false;

    private float peeTimer = 0f;

    private bool playerInRange = false; //  NEW

    void Start()
    {
        upperClosedRot = upperLid.localRotation;
        lowerClosedRot = lowerSeat.localRotation;

        upperOpenRot = Quaternion.Euler(-90f, 0f, 0f) * upperClosedRot;
        lowerOpenRot = Quaternion.Euler(-90f, 0f, 0f) * lowerClosedRot;

        if (peeParticles != null)
            peeParticles.Stop();

        if (peeUI != null)
            peeUI.SetActive(false);
    }

    void Update()
    {
        // Lid animation
        upperLid.localRotation = Quaternion.Lerp(
            upperLid.localRotation,
            upperOpened ? upperOpenRot : upperClosedRot,
            Time.deltaTime * speed
        );

        lowerSeat.localRotation = Quaternion.Lerp(
            lowerSeat.localRotation,
            lowerOpened ? lowerOpenRot : lowerClosedRot,
            Time.deltaTime * speed
        );

        // Show UI only when valid
        if (peeUI != null)
        {
            peeUI.SetActive(playerInRange && upperOpened && lowerOpened && canPee);
        }

        // Pee logic
        if (isPeeing)
        {
            peeTimer += Time.deltaTime;

            float t = peeTimer / peeDuration;

            var emission = peeParticles.emission;
            var main = peeParticles.main;

            if (t < 0.2f)
            {
                emission.rateOverTime = 120;
                main.startSpeed = 9;
            }
            else if (t < 0.7f)
            {
                emission.rateOverTime = 70;
                main.startSpeed = 6;
            }
            else
            {
                emission.rateOverTime = 20;
                main.startSpeed = 3;
            }

            if (peeTimer >= peeDuration)
            {
                StopPee();
            }
        }

        // Cooldown
        if (!canPee)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= peeCooldown)
            {
                canPee = true;
                cooldownTimer = 0f;
            }
        }

        //  G KEY (ONLY IF PLAYER NEAR)
        if (Keyboard.current.gKey.wasPressedThisFrame && playerInRange)
        {
            if (upperOpened && lowerOpened && canPee)
            {
                if (!isPeeing)
                    StartPee();
                else
                    StopPee();
            }
        }
    }

    public override void Interact()
    {
        if (isPeeing) return;

        if (!upperOpened)
        {
            upperOpened = true;
        }
        else if (!lowerOpened)
        {
            lowerOpened = true;
        }
        else
        {
            lowerOpened = false;
            upperOpened = false;
        }
    }

    void StartPee()
    {
        if (!playerInRange || !canPee) return;

        isPeeing = true;
        peeTimer = 0f;

        if (peeParticles != null)
            peeParticles.Play();

        if (playerMovement != null)
            playerMovement.enabled = false;

        Debug.Log("Start Pee");
    }

    void StopPee()
    {
        isPeeing = false;

        if (peeParticles != null)
            peeParticles.Stop();

        if (playerMovement != null)
            playerMovement.enabled = true;

        canPee = false;
        cooldownTimer = 0f;

        Debug.Log("Stop Pee");
    }

    //  TRIGGER SYSTEM
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // Auto stop if player leaves
            if (isPeeing)
                StopPee();
        }
    }
}
