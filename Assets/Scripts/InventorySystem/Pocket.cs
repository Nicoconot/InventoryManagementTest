using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pocket : MonoBehaviour
{
    public string Id = "pocket_Food";
    public string pocketName = "Food";
    public int maxSlots = 16;

    public int totalItems = 0; //temporary

    private List<InventorySlot> slots = new();
    public List<Item> pocketItems = new();

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Transform slotsParent;

    

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

        UpdateTitle(); 
        GameManager.Instance.inventoryManager.RegisterPocket(this);  
    }


    //Used for initialization
    public void LoadItems(List<Item> items)
    {
        pocketItems.Clear();
        totalItems = 0;
        List<Item> queuedItems = new();

        //We will add all items to their respective slots if they have a preset slot and it 
        // is currently free. Otherwise, queue them to be added to the next free slot.

        for(int i = 0; i < items.Count; i++)
        {
            Item currItem = items[i];
            int desiredSlot = currItem.slot;
            if(slots[desiredSlot].Item != null || desiredSlot == -1) //shouldn't happen on initialization
            {
                queuedItems.Add(currItem);
            }
            else
            {
                slots[desiredSlot].Setup(currItem);
                pocketItems.Add(currItem);
                totalItems++;
            }
        }

        foreach(var item in queuedItems)
        {
            if(totalItems < maxSlots)
            {
                //if there is space
                var nextFreeSlot = slots.FirstOrDefault(x => x.Item == null);

                if (nextFreeSlot == null)
                {
                    //no free slots found. this shouldn't happen
                    Debug.LogError($"Couldn't find a free slot in inventory for item {item.itemData.itemName}," +
                    " even though there is space. Was the total item count not updated?");
                    continue; //maybe return
                }

                item.slot = slots.IndexOf(nextFreeSlot);
                nextFreeSlot.Setup(item);
                pocketItems.Add(item);
                totalItems++;
            }
            else
            {
                //no more free space, which also shouldn't happen since this is a loading method. 
                // If trying to pick up items, the AddItem function should be used.
                Debug.LogError($"The pocket {pocketName} is full! Are you trying to pick up an item in the loading function?");
            }
        }

        pocketItems.OrderBy(x => x.slot);

        UpdateTitle();
    }

    public bool TryAddItem(Item item)
    {
        if(totalItems >= maxSlots)
        {
            //inventory is full. Do not pick up
            return false;
        }

        if(item.slot != -1)
        {
            var slot = slots[item.slot];
            if(slot.Item == null)
            {
                slot.Setup(item);
                pocketItems.Add(item);
                totalItems++;
                return true;
            }
            //if slot is already occupied, something is wrong. Item will be assigned a new slot, but this shouldn't happen.
            Debug.LogWarning($"Item {item.itemData.itemName} is assigned to slot {item.slot}, but that slot isn't free." 
            + " Assigning a new slot.");
        }

        var nextFreeSlot = slots.FirstOrDefault(x => x.Item == null);

        if (nextFreeSlot == null)
        {
            //should not happen
            Debug.LogError($"Couldn't find a free slot in inventory for item {item.itemData.itemName}," +
                    " even though there is space. Was the total item count not updated?");
            return false;
        }

        item.slot = slots.IndexOf(nextFreeSlot);
        nextFreeSlot.Setup(item);
        pocketItems.Add(item);
        totalItems++;

        UpdateTitle();

        return true;
    }

}
