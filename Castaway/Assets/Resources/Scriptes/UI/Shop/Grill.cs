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

    private bool cookingCheck = false;
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

        FoodCheck();
    }


    private void FoodCheck()
    {
        int slotCnt = Inventory.Instance.AllSlot.Count;
        int foodCntL = 0;
        int foodCntR = 0;

        for(int i = 0; i< slotCnt; i++)
        {
            Slot slot = Inventory.Instance.AllSlot[i].GetComponent<Slot>();

            if (!slot.isSlots())
                continue;


            if (slot.ItemReturn().no == subSlotL.GetComponent<Item>().no)
            {
                foodCntL += 1;
            }

            if(slot.ItemReturn().no == subSlotR.GetComponent<Item>().no)
            {
                foodCntR += 1;
            }
            
        }

        if(foodCntL == 1 && foodCntR == 1)
        {
            cookingCheck = true;
            subSlotL.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            subSlotR.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            cookingCheck = false;
            subSlotL.GetComponent<Image>().color = new Color(255, 255, 255, 100);
            subSlotR.GetComponent<Image>().color = new Color(255, 255, 255, 100);
        }

        Debug.Log(cookingCheck);

    }


    private void Cooking(bool cookingCheck)
    {



    }

}
