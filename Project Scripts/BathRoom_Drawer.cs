using UnityEngine;

public class BathroomDrawerInteract : Interactable
{
    public Transform bathroomDrawer;
    public float slideDistance = 0.3f;
    public float moveSpeed = 3f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool opened = false;

    void Start()
    {
        closedPosition = bathroomDrawer.localPosition;

        // Move only in Z direction (forward/back)
        openPosition = closedPosition + new Vector3(slideDistance, 0, 0);
    }

    void Update()
    {
        if (opened)
        {
            bathroomDrawer.localPosition = Vector3.Lerp(
                bathroomDrawer.localPosition,
                openPosition,
                Time.deltaTime * moveSpeed
            );
        }
        else
        {
            bathroomDrawer.localPosition = Vector3.Lerp(
                bathroomDrawer.localPosition,
                closedPosition,
                Time.deltaTime * moveSpeed
            );
        }
    }

    public override void Interact()
    {
        opened = !opened;
    }
}
