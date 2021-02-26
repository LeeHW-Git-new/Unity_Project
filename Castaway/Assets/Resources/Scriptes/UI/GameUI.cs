using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{

    public GameObject inventoryPanel;
    bool activeInventory = false;

    public GameObject slotHolder;

    public RingMenu MainMenuPrfab;
    protected RingMenu MainMenuInstance;

    private string BtnName;

    public GameObject settingUI;
    bool activeSettingUI = false;

    private void Start()
    {
        inventoryPanel.SetActive(activeInventory);
    }



    private void Update()
    {
       
        if( Input.GetKeyDown(KeyCode.I)||
           (inventoryPanel.activeSelf &&Input.GetKeyDown(KeyCode.Escape))||
            BtnName == "InventoryUIExit"||
            BtnName == "InventoryBtn")
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
            BtnName = "";
        }


        if(Input.GetKeyDown(KeyCode.Q) &&
            !GameObject.Find("Player").GetComponent<Player>().action)
        {        
            MainMenuPrfab.gameObject.SetActive(true);
        }



        if(BtnName == "SettingBtn" || BtnName == "SettingReturnBtn"||Input.GetKeyDown(KeyCode.Escape) ||
            (settingUI.activeSelf&&Input.GetKeyDown(KeyCode.Escape)))
        {
            activeSettingUI = !activeSettingUI;
            settingUI.SetActive(activeSettingUI);
            BtnName = "";
        }




    }

    private void MenuClick(string path)
    {
       // Debug.Log(path);
       // string[] paths = path.Split('/');
       // GetComponent<weapon>().SetPrefab(int.Parse(paths[1]), int.Parse(paths[2]));
    }



    internal void SetPrefab (int v1, int v2)
    {
        //PrefabWeapon = weapons[v1];
        //prefabFood = foods[v2];
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
