
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //scene references
    [SerializeField] private Transform pocketsParent;
    [SerializeField] private Tooltip tooltip;
    [SerializeField] private Canvas bagCanvas;
    [SerializeField] private Canvas mainUICanvas;
    [SerializeField] private Canvas temporaryElementsCanvas;
    [SerializeField] private Canvas optionsCanvas;

    [SerializeField] private TextMeshProUGUI saveLog;

    //public variables
    public Transform PocketsParent {get => pocketsParent; private set => pocketsParent = value;}
    public Canvas TemporaryElementsCanvas {get => temporaryElementsCanvas; private set => temporaryElementsCanvas = value;}
    void Awake()
    {
        GameManager.Instance.uiManager = this;
    }

    public void ToggleBagCanvas(bool active)
    {
        bagCanvas.enabled = active;
    }

    public void ToggleOptionsCanvas(bool active)
    {
        optionsCanvas.enabled = active;
    }

    public void ShowTooltip(string title, string description, Vector2 position, int buffType, int buffAmount = 0)
    {
        tooltip.UpdateTooltip(position, title, description, buffType, buffAmount);
        tooltip.Show();
    }

    public void HideTooltip()
    {
        tooltip.Hide();
    }

    public void SaveGame()
    {
        if(GameManager.Instance.gameSerializer.CreateSaveData())
        {
            saveLog.text = "Saved game successfully.";
        }
        else saveLog.text = "There was an error saving the game.";
    }

    public void LoadGame()
    {
        if(GameManager.Instance.gameSerializer.LoadSaveData())
        {
            saveLog.text = "Loaded game successfully.";
        }
        else saveLog.text = "No save file to load found.";
    }

    public void DeleteSaveFile()
    {
        if(GameManager.Instance.gameSerializer.DeleteSaveFile())
        {
            saveLog.text = "Deleted saved game successfully.";
        }
        else saveLog.text = "There is no save file to delete.";
    }
}
