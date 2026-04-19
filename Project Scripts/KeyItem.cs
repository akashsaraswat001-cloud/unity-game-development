using UnityEngine;

public class KeyItem : Interactable
{
    public string keyID;
    public bool isUsed = false;

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

    public override string GetInteractionText()
    {
        if (isUsed)
            return "";
        else
            return "Room " + keyID + " Key";
    }
}
