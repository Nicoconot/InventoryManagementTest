using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    private ScrollRect scrollRect;

    [SerializeField][Range(0,1)] private float scrollAmount = 0.3f;
    [SerializeField] private ScrollArrow minArrow, maxArrow;
    [SerializeField] private Image fadeMin, fadeMax;

    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        
        ValueChanged(scrollRect.normalizedPosition);
    }
    

    public void ValueChanged(Vector2 value)
    {
        float rawValue;
        
        if(scrollRect.horizontal) rawValue = value.x;
        else rawValue = value.y;

        minArrow.ToggleActive(!(rawValue <= 0.05f));
        maxArrow.ToggleActive(!(rawValue >= 0.95f));

        if(fadeMin != null) fadeMin.enabled = !(rawValue <= 0.05f);
        if(fadeMax != null) fadeMax.enabled = !(rawValue >= 0.95f);        
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

    private void UpdateScroll(float newPos)
    {
        newPos = Mathf.Clamp(newPos, 0, 1);

        if(scrollRect.horizontal) scrollRect.horizontalNormalizedPosition = newPos;
        else scrollRect.verticalNormalizedPosition = newPos;
    }
}
