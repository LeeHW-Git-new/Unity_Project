using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Recipe : MonoBehaviour
{
    public string recipeName;

    public Sprite recipeImg;
    public GameObject food_L;
    public GameObject food_R;

    private Vector2 recipePosition;

    private void Awake()
    {
        this.GetComponent<Image>().sprite = recipeImg;
        recipePosition = this.transform.position;
    }


}
