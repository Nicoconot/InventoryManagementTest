using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;

    void Awake()
    {
        //testing
        var color = icon.color;
        color.a = Random.Range(0.3f, 1);
        icon.color = color;
    }
}
