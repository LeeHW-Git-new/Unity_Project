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

    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt; 
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }
    
    void Start()
    {
        SlotCnt = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
