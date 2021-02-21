﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum TYPE { HP, Weapon }

    public TYPE type;
    public Sprite defaultImg;
    public int maxCnt;

    public int healHP;

    public int weaponNo;
    public string itemName;
    public void Init(string name, int maxCnt)
    {
        switch(name)
        {
            case "HP":
                type = TYPE.HP;
                break;

            case "Weapon":
                type = TYPE.Weapon;
                break;
        }

        this.itemName = name;
        this.maxCnt = maxCnt;

        //Sprite[] spr = Inventory.Instance.spr;
        //int cnt = Inventory.Instance.spr.Length;

        //for(int i = 0; i<cnt; i++)
        //{
        //    if(spr[i].name == this.itemName)
        //    {
        //        defaultImg = spr[i];
        //        break;
        //    }
        //}

    }
    private void AddItem()
    {
        if(!Inventory.Instance.AddItem(this))
        {
           // Debug.Log("inventory full");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            AddItem();
        }
    }

    public void UseItem()
    {

        switch(this.type)
        {
            case Item.TYPE.HP:
                GameManager.Instance.playerHP += this.healHP;
                break;

            case Item.TYPE.Weapon:
                break;
        }
    }

}