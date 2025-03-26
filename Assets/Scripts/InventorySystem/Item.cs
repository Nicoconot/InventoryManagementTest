
[System.Serializable]
public class Item 
{
    public ItemData itemData;

    public int slot = -1;

    public Item(ItemData data)
    {
        itemData = data;
    }

    public Item(ItemData data, int slot)
    {
        itemData = data;
        this.slot = slot;
    }
}
