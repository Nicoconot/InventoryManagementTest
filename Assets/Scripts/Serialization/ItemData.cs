using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Data/Generic Item Data")]
public class ItemData : ScriptableObject
{
    public string itemId;
    public string itemName;
    public Sprite icon;

    [TextArea] public string description; //for hover text

    public int buffAmount = 0;

    public virtual string GetPocketName()
    {
        return "";
    }
}
