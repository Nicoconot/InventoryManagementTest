using System.Collections.Generic;

[System.Serializable]
public class GameData 
{
    public List<Item> playerInventory;

    public int currentPlayerHP;
    public int currentPlayerCoins;

    public GameData(List<Item> inventory, int playerHP, int playerCoins)
    {
        playerInventory = inventory;
        currentPlayerHP = playerHP;
        currentPlayerCoins = playerCoins;
    }
}
