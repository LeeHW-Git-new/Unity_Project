using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum TYPE { HP, Weapon, Etc}

    public TYPE type;
    public Sprite defaultImg;
    public int maxCnt;

    public int healHP;

    public int no;
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

            case "Etc":
                type = TYPE.Etc;
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
        if(Inventory.Instance.AddItem(this))
            gameObject.SetActive(false);
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
                SoundManager.Instance.PlaySFX("FoodEat");
                break;

            case Item.TYPE.Weapon:
                break;

            case Item.TYPE.Etc:
                break;
        }
    }
}
