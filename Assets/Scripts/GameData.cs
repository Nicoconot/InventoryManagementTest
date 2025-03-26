using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public List<Item> playerInventory;

    public GameData(List<Item> inventory)
    {
        playerInventory = inventory;
    }

}
