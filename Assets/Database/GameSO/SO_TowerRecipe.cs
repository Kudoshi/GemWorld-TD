using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "Data_TowerRecipe", menuName = "ScriptableObject/Data_TowerRecipe")]
public class SO_TowerRecipe : ScriptableObject
{
   public TowerRecipe[] recipeList;
   public List<RecipeTracker> recipeTrackerList;

    public void SetupRecipeTracker() //Resets recipe tracker
    {
        recipeTrackerList = new List<RecipeTracker>();
        foreach (var recipe in recipeList)
        {
            Dictionary<string, GameObject> tempTwrReq = new Dictionary<string, GameObject>();
            foreach (var twrName in recipe.towersRequired)
            {
                tempTwrReq.Add(twrName, null);
            }
            RecipeTracker tempRecipe = new RecipeTracker(recipe.towerUpgrade, tempTwrReq);
            recipeTrackerList.Add(tempRecipe);
        }
    }
    public void ResetRecipeTracker()
    {
        recipeTrackerList.Clear();
        SetupRecipeTracker();
    }
    public void UpdateTempRecipeTracker(Dictionary<GameObject, string> tempTower)
    {
        foreach(var recipe in recipeTrackerList)
        {
            foreach(var twrBuilt in tempTower)
            {
                //Checks if twr is in recipe
               foreach (var twrReq in recipe.towerRequired.ToList())
                {
                    //twr built name == twrReq name
                    if (twrBuilt.Value == twrReq.Key && twrReq.Value == null)
                        InsertRecipeTrackerList(recipe.towerUpgrade, twrBuilt.Value, twrBuilt.Key);
                }

            }
        }

        CheckForTempUpgrade();
    }

    private void CheckForTempUpgrade()
    {
        foreach(var recipe in recipeTrackerList)
        {
            //Check If Twr Required is Met
            bool recipeCanCombine = true;
            foreach(var twrReq in recipe.towerRequired)
            {
                if (twrReq.Value == null)
                    recipeCanCombine = false;
            }

            //Put into dictionary if can upgrade
            if (recipeCanCombine)
            {
                foreach(var twrReq in recipe.towerRequired)
                {
                    twrReq.Value.GetComponent<TowerObject>().AddTowerList(TowerObject.TowerListType.Combinable, recipe.towerUpgrade);
                }
            }
                
        }
    }

    private void InsertRecipeTrackerList(string towerUpgrade, string towerBuilt, GameObject value)
    {
        foreach(var recipe in recipeTrackerList)
        {
            if (recipe.towerUpgrade == towerUpgrade && recipe.towerRequired[towerBuilt] == null)
            {
                recipe.towerRequired[towerBuilt] = value;
            }
        }
    }

    public void PrintRecipeTracker()
    {
        foreach (var recipe in recipeTrackerList)
        {
            Debug.Log("----------------------------------");
            Debug.Log("=====[ " + recipe.towerUpgrade + " ]=====");
            foreach (var twr in recipe.towerRequired)
            {
                Debug.Log(twr.Key + " : " + twr.Value);
            }
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintRecipeTracker();
        }
    }
}
[System.Serializable]
public class TowerRecipe
{
    public string towerUpgrade;
    public List<string> towersRequired;
}
public class RecipeTracker
{
    public string towerUpgrade;
    public Dictionary<string, GameObject> towerRequired;

    public RecipeTracker(string twrOutput, Dictionary<string, GameObject> twrRequired)
    {
        towerUpgrade = twrOutput;
        towerRequired = twrRequired;
    }
}
