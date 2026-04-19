using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    private HashSet<string> keys = new HashSet<string>();

    void Awake()
    {
        instance = this;
    }

    public void AddKey(string keyID)
    {
        keys.Add(keyID);
        Debug.Log("Picked up key: " + keyID);
    }

    public bool HasKey(string keyID)
    {
        return keys.Contains(keyID);
    }
}
