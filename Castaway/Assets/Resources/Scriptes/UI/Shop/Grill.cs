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

    private void Update()
    {
        FoodCheck();
    }
    private void SelectRecipe()
    {
        selectRecipe = EventSystem.current.currentSelectedGameObject.GetComponent<RecipeBtn>().recipe;

        mainSlot.GetComponent<Image>().sprite = selectRecipe.mainFood.GetComponent<Item>().defaultImg;
        mainSlot.GetComponent<Item>().defaultImg = selectRecipe.mainFood.GetComponent<Item>().defaultImg;
        mainSlot.GetComponent<Item>().itemName = selectRecipe.mainFood.GetComponent<Item>().itemName;
        mainSlot.GetComponent<Item>().healHP = selectRecipe.mainFood.GetComponent<Item>().healHP;
        mainSlot.GetComponent<Item>().no = selectRecipe.mainFood.GetComponent<Item>().no;

        subSlotL.GetComponent<Image>().sprite = selectRecipe.food1.mainFood.GetComponent<Item>().defaultImg;
        subSlotL.GetComponent<Item>().itemName = selectRecipe.food1.mainFood.GetComponent<Item>().itemName;
        subSlotL.GetComponent<Item>().no = selectRecipe.food1.mainFood.GetComponent<Item>().no;

        subSlotR.GetComponent<Image>().sprite = selectRecipe.food2.mainFood.GetComponent<Item>().defaultImg;
        subSlotR.GetComponent<Item>().itemName = selectRecipe.food2.mainFood.GetComponent<Item>().itemName;
        subSlotR.GetComponent<Item>().no = selectRecipe.food2.mainFood.GetComponent<Item>().no;
    }


    private void FoodCheck()
    {
        int slotCnt = Inventory.Instance.AllSlot.Count;
        bool foodCntL = false;
        bool foodCntR = false;

        for (int i = 0; i< slotCnt; i++)
        {
            Slot slot = Inventory.Instance.AllSlot[i].GetComponent<Slot>();

            if (!slot.isSlots())
                continue;


            if (slot.ItemReturn().no == subSlotL.GetComponent<Item>().no)
            {
                foodCntL = true;
            }
            
            if (slot.ItemReturn().no == subSlotR.GetComponent<Item>().no)
            {
                foodCntR = true;             
            }

            
        }

        if(foodCntL)
        {
            subSlotL.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        else
        {
            subSlotL.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }

        if (foodCntR)
        {
            subSlotR.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        else
        {
            subSlotR.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }


        if (foodCntL && foodCntR)
        {
            cookingCheck = true;
        }
        else
        {
            cookingCheck = false;
        }

    }


    private void Cooking()
    {
        if (cookingCheck)
        {
            GameObject.Find("Player").GetComponent<Animator>().SetTrigger("Cooking");
            Inventory.Instance.DeleteItem(subSlotL.GetComponent<Item>());
            Inventory.Instance.DeleteItem(subSlotR.GetComponent<Item>());
            Inventory.Instance.AddItem(mainSlot.GetComponent<Item>());
        }

    }

}
