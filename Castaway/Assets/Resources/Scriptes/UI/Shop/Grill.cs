using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Grill : MonoBehaviour
{
    private Recipe selectRecipe;

    public Image mainSlot;
    public Image subSlotL;
    public Image subSlotR;

    private void SelectRecipe()
    {
        selectRecipe = EventSystem.current.currentSelectedGameObject.GetComponent<RecipeBtn>().recipe;

        mainSlot.sprite = selectRecipe.sprite;
        subSlotL.sprite = selectRecipe.food1.sprite;
        subSlotR.sprite = selectRecipe.food2.sprite;

    }




}
