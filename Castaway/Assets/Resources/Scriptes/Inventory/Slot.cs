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

    public float doubleClickSecond = 0.25f;
    private bool isOneClick = false;
    private double Timer = 0;

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
        if (isOneClick && ((Time.time - Timer) > doubleClickSecond))
        {
            isOneClick = false;
        }

        if (!isOneClick)
        {
              Timer = Time.time;
              isOneClick = true;
        }
        else if (isOneClick && ((Time.time - Timer) < doubleClickSecond))
        {
           if (item != null)
           {
             bool isUse = item.Use();
               if (isUse)
                 Inventory.Instance.RemoveItem(slotNum);
           }

            isOneClick = false;
        }
        


    }

}
