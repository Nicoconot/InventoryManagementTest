using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pocket : MonoBehaviour
{
    public string Id = "pocket_Food";
    public string name = "Food";
    public int maxSlots = 16;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Transform slotsParent;

    private int totalItems = 0; //temporary

    void Awake()
    {
        GameManager.Instance.OnInventoryManagerReady += PopulateSlots;
    }

    void UpdateTitle()
    {
        titleText.text = $"{name}   {totalItems}/{maxSlots}";
    }

    void PopulateSlots()
    {
        GameManager.Instance.OnInventoryManagerReady -= PopulateSlots;

        var slotPrefab = GameManager.Instance.inventoryManager.RetrievePrefab("inventorySlot");

        if(slotPrefab == null) return;
        for (int i = 0; i < maxSlots; i++)
        {
            Instantiate(slotPrefab, slotsParent);
        }

        UpdateTitle();
    }

}
