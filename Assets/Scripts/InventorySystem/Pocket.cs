using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pocket : MonoBehaviour
{
    public string Id = "pocket_Food";
    public string pocketName = "Food";
    public int maxSlots = 16;

    private List<InventorySlot> slots = new();

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Transform slotsParent;

    private int totalItems = 0; //temporary

    void Awake()
    {
        GameManager.Instance.OnInventoryManagerReady += PopulateSlots;
    }

    void UpdateTitle()
    {
        titleText.text = $"{pocketName}   {totalItems}/{maxSlots}";
    }

    void PopulateSlots()
    {
        GameManager.Instance.OnInventoryManagerReady -= PopulateSlots;

        var slotPrefab = GameManager.Instance.inventoryManager.RetrievePrefab("inventorySlot");

        if(slotPrefab == null) return;
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(Instantiate(slotPrefab, slotsParent).GetComponent<InventorySlot>());
        }        

        UpdateInventory();        
    }

    void UpdateInventory()
    {
        List<Item> pocketItems = new();

        if(pocketName == "Food")
        pocketItems = GameManager.Instance.player.playerInventory.items.FindAll(x => x.itemData is FoodItemData);
        else if(pocketName == "Weapons")
        pocketItems = GameManager.Instance.player.playerInventory.items.FindAll(x => x.itemData is WeaponItemData);
        else if(pocketName == "Miscellaneous")
        pocketItems = GameManager.Instance.player.playerInventory.items.FindAll(x => x.itemData is MiscItemData);

        foreach(var item in pocketItems)
        {
            slots[item.slot].Setup(item.itemData.icon);
        }

        totalItems = pocketItems.Count;

        UpdateTitle();
    }

}
