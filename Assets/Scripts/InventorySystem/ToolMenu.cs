using UnityEngine;
using UnityEngine.UI;

public class ToolMenu : MonoBehaviour
{
    [SerializeField]private Image weaponImage;
    [SerializeField] private Image[] foodImages = new Image[3];

    [SerializeField] private Sprite defaultSprite;

    void Awake()
    {
        GameManager.Instance.OnInventoryManagerReady +=  Init;
    }

    void Init()
    {
        GameManager.Instance.inventoryManager.OnInventoryChanged += UpdateView;
    }

    void UpdateView()
    {
        var inventoryManager = GameManager.Instance.inventoryManager;
        //Getting weapon
        var weapon = inventoryManager.GetFirstWeapon();

        if (weapon == null) weaponImage.sprite = defaultSprite;
        else weaponImage.sprite = weapon.icon;

        //Getting foods
        var foods = inventoryManager.GetFirstFoods(3);

        for (int i = 0; i < foods.Length; i++)
        {
            if(foods[i] != null)
            foodImages[i].sprite = foods[i].icon;
        }
    }
}
