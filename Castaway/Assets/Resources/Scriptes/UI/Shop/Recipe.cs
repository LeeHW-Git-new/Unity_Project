using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Recipes", menuName ="Scriptable Object/Recipe Data", order = int.MaxValue)]
public class Recipe : ScriptableObject
{
    public Sprite sprite;
    public Recipe food1; 
    public Recipe food2;

}

// grill 
// script -> void (Recipe)