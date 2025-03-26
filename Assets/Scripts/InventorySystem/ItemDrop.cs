using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    //Eventually, replace this with instantiating a preset struct
    public void Setup(ItemData data, Vector2 initialPos)
    {
        itemData = data;
        transform.position = initialPos;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //add item to player inventory
            var player = GameManager.Instance.player;
            if(player.AddItemToInventory(itemData))
            {
                Destroy(gameObject);
            }
        }
    }
}
