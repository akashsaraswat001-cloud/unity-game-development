using UnityEngine;

public class CarryableItem : Interactable
{
    PlayerCarrySystem playerCarry;

    void Start()
    {
        playerCarry = FindObjectOfType<PlayerCarrySystem>();
    }

    public override void Interact()
    {
        if (playerCarry != null)
        {
            playerCarry.PickUpObject(gameObject);
        }
    }
}
