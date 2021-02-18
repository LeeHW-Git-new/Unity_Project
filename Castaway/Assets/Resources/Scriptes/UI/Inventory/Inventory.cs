using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory sInstance;

    public static Inventory Instance
    {
        get
        {
            if (sInstance == null)
            {
                Debug.Log("instance is null");
            }
            return sInstance;
        }
    }

    private void Awake()
    {
        sInstance = this;
    }
    #endregion

    public List<GameObject> AllSlot;    
    public RectTransform InvenRect;     
    public GameObject OriginSlot;       

    public float slotSize;              
    public float slotGap;               
    public float slotCountX;            
    public float slotCountY;            

    private float EmptySlot;            

    private void Start()
    {

        for (int y = 0; y < slotCountY; y++)
        {
            for (int x = 0; x < slotCountX; x++)
            {

                GameObject slot = Instantiate(OriginSlot) as GameObject;

                RectTransform slotRect = slot.GetComponent<RectTransform>();

                RectTransform item = slot.transform.GetChild(0).GetComponent<RectTransform>();

                slot.name = "slot_" + y + "_" + x;
                slot.transform.parent = transform;

                slotRect.localPosition = new Vector3((slotSize * x) + (slotGap * (x + 1)),
                                                   -((slotSize * y) + (slotGap * (y + 1))),
                                                      0);

                slotRect.localScale = Vector3.one;
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize); 
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);   

                item.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize - slotSize * 0.3f); 
                item.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize - slotSize * 0.3f);   

                AllSlot.Add(slot);
            }
        }
        EmptySlot = AllSlot.Count;
    }

    public bool AddItem(Item item)
    {
        int slotCount = AllSlot.Count;

        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = AllSlot[i].GetComponent<Slot>();

            if (!slot.isSlots())
                continue;


            if (slot.ItemReturn().type == item.type && slot.ItemMax(item))
            {
                slot.AddItem(item);
                return true;
            }
        }

        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = AllSlot[i].GetComponent<Slot>();

            if (slot.isSlots())
                continue;

            slot.AddItem(item);
            return true;
        }

        return false;
    }
}
