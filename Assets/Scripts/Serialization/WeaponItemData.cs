using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data/Weapon Item Data")]
public class WeaponItemData : ItemData
{
    public override string GetPocketName()
    {
        return "Weapons";
    }
}
