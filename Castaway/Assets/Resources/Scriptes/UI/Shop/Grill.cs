using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grill : MonoBehaviour
{
    private Recipe selectRecipe;

    public GameObject mainSlot;
    public GameObject subSlotL;
    public GameObject subSlotR;


    private void Update()
    {
        CookingTest();
    }

    private void SelectRecipe()
    {
        selectRecipe = EventSystem.current.currentSelectedGameObject.GetComponent<RecipeBtn>().recipe;

        mainSlot.GetComponent<Image>().sprite = selectRecipe.mainFood.GetComponent<Item>().defaultImg;
        mainSlot.GetComponent<Item>().itemName = selectRecipe.mainFood.GetComponent<Item>().itemName;
        mainSlot.GetComponent<Item>().no = selectRecipe.mainFood.GetComponent<Item>().no;

        subSlotL.GetComponent<Image>().sprite = selectRecipe.food1.mainFood.GetComponent<Item>().defaultImg;
        subSlotL.GetComponent<Item>().itemName = selectRecipe.food1.mainFood.GetComponent<Item>().itemName;
        subSlotL.GetComponent<Item>().no = selectRecipe.food1.mainFood.GetComponent<Item>().no;

        subSlotR.GetComponent<Image>().sprite = selectRecipe.food2.mainFood.GetComponent<Item>().defaultImg;
        subSlotR.GetComponent<Item>().itemName = selectRecipe.food2.mainFood.GetComponent<Item>().itemName;
        subSlotR.GetComponent<Item>().no = selectRecipe.food2.mainFood.GetComponent<Item>().no;

    }


    public int CookingTest()
    {
        int slotCnt = Inventory.Instance.AllSlot.Count;
        int cookCnt = 0;

        for(int i = 0; i< slotCnt; i++)
        {
            Slot slot = Inventory.Instance.AllSlot[i].GetComponent<Slot>();

            if (!slot.isSlots())
                continue;

            if(slot.ItemReturn().no == subSlotL.GetComponent<Item>().no ||
                slot.ItemReturn().no == subSlotR.GetComponent<Item>().no)
            {
                cookCnt += 1;
            }
            // slot.ItemReturn().no == subSlotR.GetComponent<Item>().no)

        }
        Debug.Log(cookCnt);
        return cookCnt;
    }




}
