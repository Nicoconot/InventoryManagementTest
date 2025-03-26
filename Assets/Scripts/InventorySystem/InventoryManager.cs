using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //For now, this is a regular instantiable class for simplicity's sake, since we only have one scene. 
    // Ideally this would be replaced with a persistent object for basic functions and the retrieval of 
    // prefabs and whatnot would be done through scriptable objects.
    
    private Player player;

    //Prefab refs
    //terrible terrible way to do this. Not modular and very difficult to scale, but it'll have to do for now
    [SerializeField] private GameObject inventorySlotPrefab;

    void Start()
    {
        GameManager.Instance.inventoryManager = this;

        GameManager.Instance.OnInventoryManagerReady?.Invoke();
    }

    public GameObject RetrievePrefab(string id)
    {
        switch (id)
        {
            case "inventorySlot":
            return inventorySlotPrefab;
            default:
            throw new Exception($"Prefab {id} does not exist.");
        }
    }
}
