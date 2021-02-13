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

}
