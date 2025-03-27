
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropHandler : MonoBehaviour
{
    [SerializeField] private RectTransform dragRt;
    [SerializeField] private Image image;

    private Item draggingItem;
    private bool dragging;

    void Awake()
    {
        GameManager.Instance.dragAndDropHandler = this;
    }

    public List<RaycastResult> RaycastMouse()
    {

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            pointerId = -1,
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results;
    }

    public void StartDragging(Item item)
    {
        draggingItem = item;
        image.sprite = item.itemData.icon;
        image.enabled = true;
        dragging = true;

        GameManager.Instance.uiManager.ToggleBagCanvasInteractable(false);
        SetRectTransformToMousePos();
    }

    private void Drop()
    {
        image.enabled = false;
        var raycastResult = RaycastMouse();

        foreach (var result in raycastResult)
        {
            if (result.gameObject.CompareTag("InventorySlot"))
            {
                var slot = result.gameObject.GetComponent<InventorySlot>();

                //first, check if slot actually belongs to the right pocket
                //eventually constrain cursor to the pocket area
                if (slot.parentPocketType == draggingItem.itemData.GetPocketName())
                {
                    //if slot is empty, move item to that slot
                    //otherwise, swap items
                    int slotIndex = slot.transform.GetSiblingIndex();
                    if (slot.Item == null)
                    {
                        GameManager.Instance.inventoryManager.TryRemoveItemFromPocket(draggingItem);
                        draggingItem.slot = slotIndex;
                        GameManager.Instance.inventoryManager.TryAddItemToPocket(draggingItem);
                    }
                    else
                    {
                        Item previousItem = slot.Item;
                        int prevItemSlotIndex = previousItem.slot;
                        int draggingSlotIndex = draggingItem.slot;
                        GameManager.Instance.inventoryManager.TryRemoveItemFromPocket(draggingItem);
                        GameManager.Instance.inventoryManager.TryRemoveItemFromPocket(previousItem);
                        previousItem.slot = draggingSlotIndex;
                        draggingItem.slot = prevItemSlotIndex;
                        GameManager.Instance.inventoryManager.TryAddItemToPocket(draggingItem);
                        GameManager.Instance.inventoryManager.TryAddItemToPocket(previousItem);

                    }
                }
                break;
            }
            if (result.gameObject.CompareTag("Trash"))
            {
                GameManager.Instance.inventoryManager.TryRemoveItemFromPocket(draggingItem);
            }
        }
        GameManager.Instance.uiManager.ToggleBagCanvasInteractable(true);
    }

    private void Update()
    {
        if (!dragging) return;

        SetRectTransformToMousePos();

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            Drop();
        }
    }

    private void SetRectTransformToMousePos()
    {
        Vector2 mousePos = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)dragRt.parent, mousePos, Camera.main, out Vector2 localPoint);

        dragRt.anchoredPosition = localPoint;
    }


}
