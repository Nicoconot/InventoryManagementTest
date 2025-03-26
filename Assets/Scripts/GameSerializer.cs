using System.IO;
using UnityEngine;

public class GameSerializer : MonoBehaviour
{
    //We will be saving and loading only the player inventory for now
    private string dataPath;

    void Awake()
    {
        GameManager.Instance.gameSerializer = this;
        dataPath = Application.persistentDataPath + "/gamedata.data";
    }
    public bool CreateSaveData()
    {
        GameData gameData = new GameData(GameManager.Instance.inventoryManager.GetAllItems());

        string jsonString = JsonUtility.ToJson(gameData);

        WriteSaveData(jsonString);

        return true;
    }

    public bool LoadSaveData()
    {
        var data = ReadSaveData();
        if(data == null) 
        {
            print("Game data null");
            return false;
        }
        GameManager.Instance.inventoryManager.LoadInventory(data.playerInventory);

        return true;
    }

    public bool DeleteSaveFile()
    {
        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
            return true;
        }

        return false;
    }
    void WriteSaveData(string data)
    {
        File.WriteAllText(dataPath, data);
    }

    GameData ReadSaveData()
    {
        if (File.Exists(dataPath))
        {
            string fileContents = File.ReadAllText(dataPath);

            GameData gameData = JsonUtility.FromJson<GameData>(fileContents);

            return gameData;
        }

        return null;
    }
}
