using UnityEngine;

public class InteractableHighlight : MonoBehaviour
{
    public GameObject sparkleEffect;

    void Start()
    {
        if (sparkleEffect != null)
        {
            sparkleEffect.SetActive(true); // ALWAYS ON
        }
    }
}
