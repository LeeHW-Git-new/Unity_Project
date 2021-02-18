using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Stack<Item> slot;  
    public Text text;       
    public Sprite DefaultImg; 

    private Image ItemImg;
    private bool isSlot;    

    public Item ItemReturn() { return slot.Peek(); } 
    public bool ItemMax(Item item) { return ItemReturn().maxCnt > slot.Count; }  
    public bool isSlots() { return isSlot; } 
    public void SetSlots(bool isSlot) { this.isSlot = isSlot; }

    void Start()
    { 
        slot = new Stack<Item>();  
        isSlot = false;
       // float Size = text.gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta.x;
        //text.fontSize = (int)(Size * 0.5f);
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
        Debug.Log("itemIn");
        SetSlots(isSlot);
        ItemImg.color = new Color(255, 255, 255, 1.0f);
        ItemImg.sprite = sprite;
       // text.text = slot.Count > 1 ? slot.Count.ToString() : "";
        // ItemIO.SaveDate();  
    }
}
