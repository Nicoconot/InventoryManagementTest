using System;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float scrollAmount = 0.3f;
    [SerializeField] private ScrollArrow minArrow, maxArrow;
    [SerializeField] private Image fadeMin, fadeMax;

    private ScrollRect scrollRect;
    private bool isExpanded;

    public bool IsExpanded { get => isExpanded; private set => isExpanded = value; }

    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();

        ValueChanged(scrollRect.normalizedPosition);
    }

    public void ValueChanged(Vector2 value)
    {
        if (isExpanded) return;
        float rawValue;

        if (scrollRect.horizontal) rawValue = value.x;
        else rawValue = value.y;

        minArrow.ToggleActive(!(rawValue <= 0.05f));
        maxArrow.ToggleActive(!(rawValue >= 0.95f));

        if (fadeMin != null) fadeMin.enabled = !(rawValue <= 0.05f);
        if (fadeMax != null) fadeMax.enabled = !(rawValue >= 0.95f);
    }

    //Called from buttons
    public void ScrollBack()
    {
        float currValue = scrollRect.horizontal ?
                            scrollRect.horizontalNormalizedPosition
                            : scrollRect.verticalNormalizedPosition;

        float newPos = currValue - scrollAmount;

        UpdateScroll(newPos);
    }

    public void ScrollForward()
    {
        float currValue = scrollRect.horizontal ?
                            scrollRect.horizontalNormalizedPosition
                            : scrollRect.verticalNormalizedPosition;

        float newPos = currValue + scrollAmount;

        UpdateScroll(newPos);
    }

    public void ToggleView()
    {
        if (!isExpanded) ExpandView();
        else ContractView();
    }

    private void ExpandView()
    {
        isExpanded = true;
        var content = scrollRect.content;
        var gridGroup = content.GetComponent<GridLayoutGroup>();
        var contentSizeFitter = content.GetComponent<ContentSizeFitter>();
        var scrollRectTransform = (RectTransform)scrollRect.transform;
        var scrollSizeDelta = scrollRectTransform.sizeDelta;
        var pocketGroupRt = (RectTransform)scrollRectTransform.parent;
        var pocketGroupParentRt = (RectTransform)scrollRectTransform.parent.parent;
        var rootRt = (RectTransform)pocketGroupParentRt.parent;

        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        content.sizeDelta = new Vector2(scrollSizeDelta.x, content.sizeDelta.y);

        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        gridGroup.constraint = GridLayoutGroup.Constraint.Flexible;

        LayoutRebuilder.ForceRebuildLayoutImmediate(content);

        float scrollYSize = gridGroup.preferredHeight;

        scrollRectTransform.sizeDelta = new Vector2(scrollSizeDelta.x, scrollYSize);

        LayoutRebuilder.ForceRebuildLayoutImmediate(pocketGroupRt);
        pocketGroupParentRt.sizeDelta = new Vector2(scrollSizeDelta.x, pocketGroupRt.sizeDelta.y);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rootRt);

        minArrow.ToggleActive(false);
        maxArrow.ToggleActive(false);

        if (fadeMin != null && fadeMax != null)
        {
            fadeMin.enabled = false;
            fadeMax.enabled = false;
        }
    }

    private void ContractView()
    {

        var content = scrollRect.content;
        var gridGroup = content.GetComponent<GridLayoutGroup>();
        var contentSizeFitter = content.GetComponent<ContentSizeFitter>();
        var scrollRectTransform = (RectTransform)scrollRect.transform;
        var scrollSizeDelta = scrollRectTransform.sizeDelta;
        var pocketGroupRt = (RectTransform)scrollRectTransform.parent;
        var pocketGroupParentRt = (RectTransform)scrollRectTransform.parent.parent;
        var rootRt = (RectTransform)pocketGroupParentRt.parent;

        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.MinSize;

        gridGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridGroup.constraintCount = 1;

        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

        LayoutRebuilder.ForceRebuildLayoutImmediate(content);

        float scrollYSize = gridGroup.preferredHeight;

        scrollRectTransform.sizeDelta = new Vector2(scrollSizeDelta.x, scrollYSize);

        LayoutRebuilder.ForceRebuildLayoutImmediate(pocketGroupRt);
        pocketGroupParentRt.sizeDelta = new Vector2(scrollSizeDelta.x, pocketGroupRt.sizeDelta.y);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rootRt);

        isExpanded = false;

        ValueChanged(scrollRect.normalizedPosition);
    }

    private void UpdateScroll(float newPos)
    {
        if (isExpanded) return;
        newPos = Mathf.Clamp(newPos, 0, 1);

        if (scrollRect.horizontal) scrollRect.horizontalNormalizedPosition = newPos;
        else scrollRect.verticalNormalizedPosition = newPos;
    }
}
