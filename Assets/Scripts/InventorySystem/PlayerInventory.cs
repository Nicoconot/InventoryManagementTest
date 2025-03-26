using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items;

    void Awake()
    {
        GameManager.Instance.OnInventoryPocketsReady += Init;
    }
    public void Init()
    {
        GameManager.Instance.OnInventoryPocketsReady -= Init;
        //Temporary. We have a debug list for now, but eventually it'll be replaced with the actual loading function
        GameManager.Instance.inventoryManager.LoadInventory(items);
    }

    public bool AddItem(ItemData itemData)
    {
        bool success = GameManager.Instance.inventoryManager.TryAddItemToPocket(itemData);
        if(success) UpdateInventory();
        return success;
    }

    public void RemoveItem()
    {

    }

    public void UpdateInventory()
    {
        items = GameManager.Instance.inventoryManager.GetAllItems();
    }
}
