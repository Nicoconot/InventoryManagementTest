using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private RectTransform rt;
    private CanvasGroup canvasGroup;

    private float originalSizeY;
    [SerializeField] private Sprite[] sprites = new Sprite[2];
    [SerializeField] private TextMeshProUGUI descriptionText, buffText, titleText;
    [SerializeField] private Image icon;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        originalSizeY = rt.sizeDelta.y;
    }
    public void UpdateTooltip(Vector2 position, string title, string description, int buffType, int buffAmount = 0)
    {
        descriptionText.text = description;
        titleText.text = title;        

        if(buffType != -1)
        {
            icon.enabled = true;
            icon.sprite = sprites[buffType];
            buffText.text = (buffAmount > 0 ? "+" : "-") + buffAmount;
        }
        else
        {
            icon.enabled = false;
            buffText.text = "";
        }

        //Converting positions
        RectTransform canvasRt = (RectTransform)GameManager.Instance.uiManager.TemporaryElementsCanvas.transform;

        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(position);
        Vector2 slot_screenPos = new Vector2(
        (viewportPosition.x * canvasRt.sizeDelta.x) - (canvasRt.sizeDelta.x * 0.5f),
        (viewportPosition.y * canvasRt.sizeDelta.y) - (canvasRt.sizeDelta.y * 0.5f));

        float verticalOffset = slot_screenPos.y -  originalSizeY + 10; //this is to counter the varying size of the tooltip

        rt.anchoredPosition = new Vector2(slot_screenPos.x, verticalOffset);
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
    }
}
