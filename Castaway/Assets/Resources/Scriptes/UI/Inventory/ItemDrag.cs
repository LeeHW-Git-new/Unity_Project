using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemDrag : MonoBehaviour
{
    public Transform img;

    private Image emptyImg;
    private Slot slot;

    private void Start()
    {
        slot = GetComponent<Slot>();
        img = GameObject.FindGameObjectWithTag("TempImg").transform;
        emptyImg = img.GetComponent<Image>();
    }
    public void Down()
    {
        if (!slot.isSlots())
            return;
        if(Input.GetMouseButtonDown(1))
        {
            slot.ItemReturn().UseItem();
            slot.ItemUse();
            return;
        }
        img.gameObject.SetActive(true);
        float imgSize = slot.transform.GetComponent<RectTransform>().sizeDelta.x;
        emptyImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, imgSize);
        emptyImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imgSize);
        emptyImg.sprite = slot.ItemReturn().defaultImg;
        img.transform.position = Input.mousePosition;
        slot.UpdateInfo(true, slot.DefaultImg);
        slot.text.text = "";
    }
    public void Drag()
    {
        if (!slot.isSlots())
            return;
        img.transform.position = Input.mousePosition;
    }
    public void DragEnd()
    {
        if (!slot.isSlots())
            return;
        Inventory.Instance.Swap(slot, img.transform.position);
    }
    public void Up()
    {
        if (!slot.isSlots())
            return;
        img.gameObject.SetActive(false);
        slot.UpdateInfo(true, slot.slot.Peek().defaultImg);
    }

}
