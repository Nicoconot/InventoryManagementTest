using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    //For now, this is a regular instantiable class for simplicity's sake, since we only have one scene. 
    // Ideally this would be replaced with a persistent object for basic functions and the retrieval of 
    // prefabs and whatnot would be done through scriptable objects.
    

    //Prefab refs
    //terrible terrible way to do this. Not modular and very difficult to scale, but it'll have to do for now
    [SerializeField] private GameObject inventorySlotPrefab;

    [SerializeField] private List<Pocket> pockets = new();
    private int totalPockets, loadedPockets;

    public UnityAction OnInventoryChanged;

    void Start()
    {
        GameManager.Instance.inventoryManager = this;

        totalPockets = GameManager.Instance.uiManager.PocketsParent.childCount;

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

    public void RegisterPocket(Pocket pocket)
    {
        pockets.Add(pocket);

        loadedPockets++;

        if(loadedPockets >=  totalPockets) GameManager.Instance.OnInventoryPocketsReady?.Invoke();
    }

    public void LoadInventory(List<Item> allItems)
    {
        foreach(var pocket in pockets)
        {
            pocket.ClearItems();
            var pocketItems = allItems.FindAll(x => x.itemData.GetPocketName() == pocket.pocketName);
            pocket.LoadItems(pocketItems);
        }

        OnInventoryChanged?.Invoke();
    }

    public bool TryAddItemToPocket(ItemData item)
    {
        Pocket pocket = pockets.Find(x => x.pocketName == item.GetPocketName());

        if(pocket == null)
        {
            Debug.LogError($"Could not find pocket '{item.GetPocketName()}' in inventory manager.");
            return false;
        }

        bool success = pocket.TryAddItem(new Item(item));
        OnInventoryChanged?.Invoke();
        return success;
    }

    public bool TryAddItemToPocket(Item item)
    {
        Pocket pocket = pockets.Find(x => x.pocketName == item.itemData.GetPocketName());

        if(pocket == null)
        {
            Debug.LogError($"Could not find pocket '{item.itemData.GetPocketName()}' in inventory manager.");
            return false;
        }

        bool success = pocket.TryAddItem(item);
        OnInventoryChanged?.Invoke();
        return success;
    }

    public bool TryRemoveItemFromPocket(Item item)
    {
        Pocket pocket = pockets.Find(x => x.pocketName == item.itemData.GetPocketName());

        if(pocket == null)
        {
            Debug.LogError($"Could not find pocket '{item.itemData.GetPocketName()}' in inventory manager.");
            return false;
        }

        bool success = pocket.TryRemoveItem(item);

        OnInventoryChanged?.Invoke();

        return success;
    }

    public List<Item> GetAllItems()
    {
        List<Item> allItems = new();

        foreach(var pocket in pockets)
        {
            allItems.AddRange(pocket.pocketItems);
        }

        return allItems;
    }

    public Item GetFirstWeapon()
    {
        var item = pockets.Find(x => x.pocketName == "Weapons").pocketItems.FirstOrDefault<Item>();
        if (item == null) return null;
        else return item;
    }

    public Item[] GetFirstFoods(int amount = 3)
    {
        Item[] foods = new Item[amount];

        var foodList = pockets.Find(x => x.pocketName == "Food").pocketItems;

        var foodListArray  =  foodList.OrderBy(x => x.slot).ToArray();

        for(int i = 0; i < amount; i++)
        {
            if (i >= foodListArray.Length) break;
            foods[i] = foodListArray[i];
        }

        return foods;
    }

    public bool CheckIfPocketIsExpanded(string pocketName)
    {
        var pocket = pockets.Find(x => x.pocketName == pocketName);
        if (pocket == null) return false;

        return pocket.IsExpanded();
    }
}
