using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerUpHandler
{
    public int slotNum;
    public Item item;
    public Image itemIcon;
    public Text itemText;

    private void Start()
    {
        itemIcon.color = new Color(255, 255, 255, 0);    
    }

    private void Update()
    {
        itemText.GetComponent<Text>().text = "Test";
    }

    public void UpdateSlotUI()
    {

        itemIcon.color = new Color(itemIcon.color.r, itemIcon.color.g, itemIcon.color.b, 1.0f);
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }


    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(item!=null)
        {
            bool isUse = item.Use();
            if(isUse)
                Inventory.Instance.RemoveItem(slotNum);
        }
    }
}
