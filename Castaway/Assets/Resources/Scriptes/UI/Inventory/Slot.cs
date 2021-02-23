using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Stack<Item> slot;  
    public Text text;
    private int textSize = 30;
    public Sprite DefaultImg; 


    private Image ItemImg;
    private bool isSlot;    

    public Item ItemReturn() { return slot.Peek(); } 
    public bool ItemMax(Item item) { return ItemReturn().maxCnt > slot.Count; }  
    public bool isSlots() { return isSlot; }

    private void Awake()
    {
        slot = new Stack<Item>();
        isSlot = false;
        text.fontSize = textSize;
        ItemImg = transform.GetChild(0).GetComponent<Image>();
    }



    public void AddItem(Item item)
    {
        slot.Push(item);
        UpdateInfo(true, item.defaultImg);
    }


    public void ItemUse()
    {
        if (!isSlot)
            return;

        if (slot.Count == 1)
        {      
            slot.Clear();
            UpdateInfo(false, DefaultImg);
            return;
        }

        slot.Pop();
        UpdateInfo(isSlot, ItemImg.sprite);
    }

    public void UpdateInfo(bool isSlot, Sprite sprite)
    {
        this.isSlot = isSlot;
        ItemImg.sprite = sprite;
        text.text = slot.Count > 1 ? slot.Count.ToString() : "";
        ItemIO.SaveData();  
    }
}
