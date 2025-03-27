using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    private PlayerInventory playerInventory;
    protected override void Awake()
    {
        GameManager.Instance.player = this;
        playerInventory = GetComponent<PlayerInventory>();
        base.Awake();
    }

    public bool AddItemToInventory(ItemData itemData)
    {
        controller.SetTrigger("pickup");
        return playerInventory.AddItem(itemData);
    }

    void Update()
    {
        GetMovement();
    }

    protected override void GetMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        isMoving = horizontal != 0 || vertical != 0;
    }
}
