using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new();

    void Awake()
    {
        GameManager.Instance.OnInventoryPocketsReady += Init;
    }
    public void Init()
    {
        GameManager.Instance.OnInventoryPocketsReady -= Init;
        GameManager.Instance.inventoryManager.OnInventoryChanged += UpdateInventory;
    }

    public bool AddItem(ItemData itemData)
    {
        return GameManager.Instance.inventoryManager.TryAddItemToPocket(itemData);
    }

    public void UpdateInventory()
    {
        items = GameManager.Instance.inventoryManager.GetAllItems();
    }
}
