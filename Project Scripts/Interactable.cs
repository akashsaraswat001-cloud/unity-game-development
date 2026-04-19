using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string objectName = "Object";

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + objectName);
    }

    public virtual string GetInteractionText()   
    {
        return objectName;
    }
}
