using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory playerinventory;
    public GameObject inventoryPanel;
    bool activeInventory = false;

    public Slot[] slots;
    public GameObject slotHolder;

    public RingMenu MainMenuPrfab;
    protected RingMenu MainMenuInstance;
    [HideInInspector]
    public ControllerMode mode;

    


    private void Start()
    {
        playerinventory = Inventory.Instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        playerinventory.onSlotCountChange += SlotChange;
        playerinventory.onChangeItem += RedrawSlotUI;
        inventoryPanel.SetActive(activeInventory);
    }

 

    private void SlotChange(int val)
    {
        for(int i = 0; i<slots.Length; i++)
        {
            slots[i].slotNum = i;
            if(i<playerinventory.SlotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }

        if(inventoryPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(false);
        }


        if(Input.GetKeyDown(KeyCode.Q))
        {
            MainMenuPrfab.gameObject.SetActive(true);
            //MainMenuInstance.callback = MenuClick;
        }
        else if(Input.GetKeyUp(KeyCode.Q))
        {
            MainMenuPrfab.gameObject.SetActive(false);
        }



    }

    private void MenuClick(string path)
    {
       // Debug.Log(path);
       // string[] paths = path.Split('/');
       // GetComponent<weapon>().SetPrefab(int.Parse(paths[1]), int.Parse(paths[2]));
    }

    public void AddSlot()
    {
        playerinventory.SlotCnt = playerinventory.SlotCnt + 3;
    }

    private void RedrawSlotUI()
    {
        for(int i = 0; i<slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i = 0;i<playerinventory.items.Count; i++)
        {
            slots[i].item = playerinventory.items[i];
            slots[i].UpdateSlotUI();
        }
    }


    public enum ControllerMode
    {
        Play,
        Build,
        Menu
    }

    internal void SetPrefab (int v1, int v2)
    {
        //PrefabWeapon = weapons[v1];
        //prefabFood = foods[v2];
    }


}
