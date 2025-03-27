using UnityEngine;
using UnityEngine.UI;

public class ToolMenu : MonoBehaviour
{
    [SerializeField]private Image weaponImage;
    [SerializeField] private Image[] foodImages = new Image[3];

    [SerializeField] private Sprite defaultSprite;

    private Item[] quickFoods = new Item[3];
    private Item quickWeapon;

    void Awake()
    {
        GameManager.Instance.OnInventoryManagerReady +=  Init;
        GameManager.Instance.toolMenu = this;
    }

    void Init()
    {
        GameManager.Instance.inventoryManager.OnInventoryChanged += UpdateView;
    }

    public void UseQuickWeapon()
    {
        if(quickWeapon.itemData == null) return;
        GameManager.Instance.player.Attack();
    }

    public void UseFoodItem(int index)
    {
        if(quickFoods[index].itemData == null) return;

        GameManager.Instance.player.RecoverHealth(quickFoods[index].itemData.buffAmount);
        GameManager.Instance.inventoryManager.TryRemoveItemFromPocket(quickFoods[index]);
    }

    void Clear()
    {
        weaponImage.sprite = defaultSprite;
        quickWeapon = null;

        foreach(var foodImg in foodImages)
        {
            foodImg.sprite = defaultSprite;
        }

        for(int i = 0; i < foodImages.Length; i++)
        {
            foodImages[i].sprite = defaultSprite;
            quickFoods[i] = null;
        }
    }

    void UpdateView()
    {
        Clear();
        var inventoryManager = GameManager.Instance.inventoryManager;
        //Getting weapon
        var weapon = inventoryManager.GetFirstWeapon();

        weaponImage.sprite = weapon.itemData.icon;
        quickWeapon = weapon;

        //Getting foods
        var foods = inventoryManager.GetFirstFoods(3);

        for (int i = 0; i < foods.Length; i++)
        {
            if (foods[i] != null)
            {
                quickFoods[i] = foods[i];
                foodImages[i].sprite = foods[i].itemData.icon;
            }
        }
    }
}
