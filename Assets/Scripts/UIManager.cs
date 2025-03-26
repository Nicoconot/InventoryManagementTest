
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //scene references
    [SerializeField] private Transform pocketsParent;
    [SerializeField] private Tooltip tooltip;
    [SerializeField] private Canvas bagCanvas;
    [SerializeField] private Canvas mainUICanvas;
    [SerializeField] private Canvas temporaryElementsCanvas;

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

    public void ShowTooltip(string title, string description, Vector2 position, int buffType, int buffAmount = 0)
    {
        tooltip.UpdateTooltip(position, title, description, buffType, buffAmount);
        tooltip.Show();
    }

    public void HideTooltip()
    {
        tooltip.Hide();
    }
}
