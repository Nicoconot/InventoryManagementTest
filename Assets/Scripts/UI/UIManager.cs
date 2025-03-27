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
    [SerializeField] private TextMeshProUGUI playerHealthText, uiHealthText;

    //public variables
    public Transform PocketsParent { get => pocketsParent; private set => pocketsParent = value; }
    public Canvas TemporaryElementsCanvas { get => temporaryElementsCanvas; private set => temporaryElementsCanvas = value; }
    private void Awake()
    {
        GameManager.Instance.uiManager = this;
        GameManager.Instance.OnInventoryPocketsReady += LoadGame;
    }

    #region Canvas management
    public void ToggleBagCanvas(bool active)
    {
        bagCanvas.enabled = active;
    }

    public void ToggleOptionsCanvas(bool active)
    {
        optionsCanvas.enabled = active;
    }

    public void ToggleBagCanvasInteractable(bool active)
    {
        bagCanvas.GetComponent<CanvasGroup>().interactable = active;
    }
    #endregion

    #region Tooltip management
    public void ShowTooltip(string title, string description, Vector2 position, int buffType, int buffAmount = 0)
    {
        tooltip.UpdateTooltip(position, title, description, buffType, buffAmount);
        tooltip.Show();
    }

    public void HideTooltip()
    {
        tooltip.Hide();
    }
    #endregion

    #region Save actions
    public void SaveGame()
    {
        if (GameManager.Instance.gameSerializer.CreateSaveData())
        {
            saveLog.text = "Saved game successfully.";
        }
        else saveLog.text = "There was an error saving the game.";
    }

    public void LoadGame()
    {
        GameManager.Instance.OnInventoryPocketsReady -= LoadGame;
        if (GameManager.Instance.gameSerializer.LoadSaveData())
        {
            saveLog.text = "Loaded game successfully.";
        }
        else saveLog.text = "No save file to load found.";
    }

    public void DeleteSaveFile()
    {
        if (GameManager.Instance.gameSerializer.DeleteSaveFile())
        {
            saveLog.text = "Deleted saved game successfully.";
        }
        else saveLog.text = "There is no save file to delete.";
    }
    #endregion

    #region Drag and drop
    public void StartDragging(Item item)
    {
        GameManager.Instance.dragAndDropHandler.StartDragging(item);
    }
    #endregion

    #region General UI Management
    public void UpdateHealthText(int hp, int maxHp)
    {
        string text = $": {hp}/{maxHp}";
        playerHealthText.text = "Health" + text;
        uiHealthText.text = "HP" + text;
    }
    #endregion

    #region Other Ui Generated Actions
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
