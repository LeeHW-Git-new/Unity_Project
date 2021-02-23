using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBtn : MonoBehaviour
{
    public Recipe recipe;

    private void Start()
    {
        this.GetComponent<Image>().sprite = recipe.sprite;
    }

    

}
