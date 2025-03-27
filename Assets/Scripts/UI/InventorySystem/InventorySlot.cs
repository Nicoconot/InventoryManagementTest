using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Sprite emptySprite;
    private Item item;
    private RectTransform rt;

    private bool isPointerDown;

    public Item Item {get => item; private set => item = value; }

    public string parentPocketType;

    void Awake()
    {
        rt = (RectTransform)transform;
    }

    public void Setup(string pocketType, Item item = null)
    {
        parentPocketType = pocketType;
        if (item == null) return;
        icon.sprite = item.itemData.icon;
        this.item = item;        
    }

    public void Clear()
    {
        icon.sprite = emptySprite;
        item = null;
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

    private void PickUpItem()
    {
        bool proceed =  GameManager.Instance.inventoryManager.CheckIfPocketIsExpanded(item.itemData.GetPocketName());

        if(!proceed) return;

        GameManager.Instance.uiManager.StartDragging(item);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;

        StopCoroutine(DragCooldown());
        StartCoroutine(DragCooldown());
    }

    private IEnumerator DragCooldown()
    {
        yield return new WaitForSeconds(.3f);

        if(isPointerDown && item != null) PickUpItem();
    }
}
