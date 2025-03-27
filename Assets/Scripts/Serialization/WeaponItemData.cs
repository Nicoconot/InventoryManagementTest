using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data/Weapon Item Data")]
public class WeaponItemData : ItemData
{
    public override string GetPocketName()
    {
        return "Weapons";
    }
}
