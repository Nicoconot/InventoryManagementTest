using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;
    private Item item;
    private RectTransform rt;

    public Item Item {get => item; private set => item = value; }

    void Awake()
    {
        rt = (RectTransform)transform;
    }

    public void Setup(Item item = null)
    {
        if (item == null) return;
        icon.sprite = item.itemData.icon;
        this.item = item;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item == null) return;
        Vector2 pos = rt.position;
        int buffType = item.itemData.GetPocketName() switch
        {
            "Food" => 0,
            "Weapons" => 1,
            _ => -1
        };
        GameManager.Instance.uiManager.ShowTooltip(item.itemData.itemName, 
        item.itemData.description, pos, buffType, item.itemData.buffAmount);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.uiManager.HideTooltip();
    }
}
