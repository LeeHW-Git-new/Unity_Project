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

    public GameObject parent;
    public List<GameObject> AllSlot;    
    public RectTransform InvenRect;     
    public GameObject OriginSlot;       
    public float slotSize;              
    public float slotGap;               
    public float slotCountX;            
    public float slotCountY;            


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
                slotRect.transform.SetParent(parent.transform);

                slotRect.localScale = Vector3.one;
                slotRect.localPosition = new Vector3((slotSize * x) + (slotGap * (x + 1)),
                                                   -((slotSize * y) + (slotGap * (y + 1))),
                                                      0);

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                item.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize - slotSize * 0.3f);
                item.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize - slotSize * 0.3f);

                AllSlot.Add(slot);
            }
        }
    }

    public void DeleteItem(Item item)
    {
        int slotCount = AllSlot.Count;
        for(int i = 0; i<slotCount; i++)
        {
            Slot slot = AllSlot[i].GetComponent<Slot>();
            if (!slot.isSlots())
                continue;

            if(slot.ItemReturn().no == item.no)
            {
                slot.ItemUse();
            }

        }
    }

    public bool AddItem(Item item)
    {
        if (item.type != Item.TYPE.Weapon)
        {
            int slotCount = AllSlot.Count;

            for (int i = 0; i < slotCount; i++)
            {
                Slot slot = AllSlot[i].GetComponent<Slot>();

                if (!slot.isSlots())
                    continue;


                if (slot.ItemReturn().itemName == item.itemName && slot.ItemMax(item))
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
        }
         return false;
    }



    public Slot NearDisSlot(Vector3 Pos)
    {
        float min = 10000f;
        int index = -1;
        int cnt = AllSlot.Count;
        for(int i = 0; i<cnt; i++)
        {
            Vector2 sPos = AllSlot[i].transform.GetChild(0).position;
            float dis = Vector2.Distance(sPos, Pos);

            if(dis< min)
            {
                min = dis;
                index = i;
            }
        }

        if (min > slotSize)
            return null;

        return AllSlot[index].GetComponent<Slot>();
    }

    public void Swap(Slot slot, Vector3 pos)
    {
        Slot firstSlot = NearDisSlot(pos);

        if(slot == firstSlot||firstSlot == null)
        {
            slot.UpdateInfo(true, slot.slot.Peek().defaultImg);
            return;
        }

        if(!firstSlot.isSlots())
        {
            Swap(firstSlot, slot);
        }
        else
        {
            int cnt = slot.slot.Count;
            Item item = slot.slot.Peek();
            Stack<Item> temp = new Stack<Item>();

            {
                for(int i =0; i<cnt; i++)
                {
                    temp.Push(item);
                }

                slot.slot.Clear();
            }

            Swap(slot, firstSlot);

            {
                cnt = temp.Count;
                item = temp.Peek();

                for(int i = 0; i<cnt; i++)
                {
                    firstSlot.slot.Push(item);
                }

                firstSlot.UpdateInfo(true, temp.Peek().defaultImg);
            }
        }
    }

    private void Swap(Slot tempSlot, Slot fullSlot)
    {
        int cnt = fullSlot.slot.Count;
        Item item = fullSlot.slot.Peek();

        for(int i = 0; i< cnt; i++)
        {
            if(tempSlot != null)
            {
                tempSlot.slot.Push(item);
            }
        }

        if(tempSlot != null)
        {
            tempSlot.UpdateInfo(true, fullSlot.ItemReturn().defaultImg);
        }

        fullSlot.slot.Clear();
        fullSlot.UpdateInfo(false, fullSlot.DefaultImg);
    }


}
