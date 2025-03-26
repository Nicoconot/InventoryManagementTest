using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data/Food Item Data")]
public class FoodItemData : ItemData
{
    public override string GetPocketName()
    {
        return "Food";
    }
}
