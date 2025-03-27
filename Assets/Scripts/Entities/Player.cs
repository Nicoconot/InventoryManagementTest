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

    protected override void GetMovement()
    {
        if(!canMove) return;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        isMoving = horizontal != 0 || vertical != 0;
    }

    public override void SetHP(int amount)
    {
        base.SetHP(amount);
        GameManager.Instance.uiManager.UpdateHealthText(currentHp, maxHp);
    }

    public override void TakeDamage(int dmgAmount)
    {
        base.TakeDamage(dmgAmount);
        GameManager.Instance.uiManager.UpdateHealthText(currentHp, maxHp);
    }

    public override void RecoverHealth(int hpAmount)
    {
        base.RecoverHealth(hpAmount);
        GameManager.Instance.uiManager.UpdateHealthText(currentHp, maxHp);
    }

    void Update()
    {
        GetMovement();

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.toolMenu.UseFoodItem(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Instance.toolMenu.UseFoodItem(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.Instance.toolMenu.UseFoodItem(2);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.toolMenu.UseQuickWeapon();
        }
    }
}
