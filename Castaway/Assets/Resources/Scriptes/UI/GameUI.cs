using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{
    public GameObject slotHolder;
    public GameObject inventoryPanel;
    public GameObject settingUI;
    public RingMenu MainMenuPrfab;

    protected RingMenu MainMenuInstance;

    private bool activeInventory = false;
    private bool activeSettingUI = false;
    private string BtnName;

    private void Awake()
    {
        inventoryPanel.SetActive(activeInventory);
    }


    private void Update()
    {
        InventoryEvent();
        QuickSlotCall();
    }

    private void InventoryEvent()
    {
        if (Input.GetKeyDown(KeyCode.I) ||
          (inventoryPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape)) ||
           BtnName == "InventoryUIExit" ||
           BtnName == "InventoryBtn")
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
            BtnName = "";
        }

        if (BtnName == "SettingBtn" || 
            BtnName == "SettingReturnBtn" || 
            Input.GetKeyDown(KeyCode.Escape) ||
            (settingUI.activeSelf && Input.GetKeyDown(KeyCode.Escape)))
        {
            activeSettingUI = !activeSettingUI;
            settingUI.SetActive(activeSettingUI);
            BtnName = "";
        }

    }

    private void QuickSlotCall()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !GameObject.Find("Player").GetComponent<Player>().action)
        {
            MainMenuPrfab.gameObject.SetActive(true);
        }
    }

    private void BtnEvent()
    {
        BtnName = EventSystem.current.currentSelectedGameObject.name;
    }

    private void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
