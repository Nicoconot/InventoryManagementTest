using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//THIS SCRIPT *MUST* BE SET TO HAVE A SCRIPT EXECUTION ORDER BEFORE THE DEFAULT TIME
//This is to avoid unnecessary management of awake/start functions when debugging things that 
// should be executed later on anyway and to save time
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InventoryManager inventoryManager;
    public UIManager uiManager;

    public DragAndDropHandler dragAndDropHandler;
    public ToolMenu toolMenu;

    public GameSerializer gameSerializer;
    public Player player;

    public UnityAction OnInventoryManagerReady;
    public UnityAction OnInventoryPocketsReady;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    
}
