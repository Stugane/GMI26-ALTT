using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Ingredient
{
    public AItemSO item;
    public int amount;
}
[CreateAssetMenu(fileName = "Recipe", menuName = "NewRecipe")]

public class ARecipe : ScriptableObject
{
    //public List<Ingredient> ingredients;
    public Ingredient[] ingredients = new Ingredient[0]; //Change to a higher number if you want more ingredients in a recipe
    public AItemSO result;
    public int resultAmount = 1;
}
