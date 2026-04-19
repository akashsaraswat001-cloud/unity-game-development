using UnityEngine;

public class DrawerInteract : Interactable
{
    public Transform drawer;
    public float openDistance = 0.4f;
    public float speed = 3f;

    Vector3 closedPos;
    Vector3 openPos;
    bool isOpen = false;

    void Start()
    {
        closedPos = drawer.localPosition;
        openPos = closedPos + new Vector3(0, 0, -openDistance);
    }

    void Update()
    {
        if (isOpen)
        {
            drawer.localPosition = Vector3.Lerp(drawer.localPosition, openPos, Time.deltaTime * speed);
        }
        else
        {
            drawer.localPosition = Vector3.Lerp(drawer.localPosition, closedPos, Time.deltaTime * speed);
        }
    }

    public override void Interact()
    {
        isOpen = !isOpen;
    }
}
