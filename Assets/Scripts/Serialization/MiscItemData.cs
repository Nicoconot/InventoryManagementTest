using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data/Misc Item Data")]
public class MiscItemData : ItemData
{
    public override string GetPocketName()
    {
        return "Miscellaneous";
    }
}
