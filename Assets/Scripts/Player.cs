using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    public PlayerInventory playerInventory;
    protected override void Awake()
    {
        GameManager.Instance.player = this;
        base.Awake();
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
