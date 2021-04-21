using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "Data_TowerRecipe", menuName = "ScriptableObject/Data_TowerRecipe")]
public class SO_TowerRecipe : ScriptableObject
{
    public TowerRecipe[] recipeList;
    public List<RecipeTracker> tempRecipeTrackerList;

    public void SetupRecipeTracker() //Resets recipe tracker
    {
        tempRecipeTrackerList = new List<RecipeTracker>();
        foreach (var recipe in recipeList)
        {
            Dictionary<string, bool> tempTwrReq = new Dictionary<string, bool>();
            foreach (var twrName in recipe.towersRequired)
            {
                tempTwrReq.Add(twrName, false);
            }
            RecipeTracker tempRecipe = new RecipeTracker(recipe.towerUpgrade, tempTwrReq);
            tempRecipeTrackerList.Add(tempRecipe);
        }
    }
    public void ResetRecipeTracker()
    {
        tempRecipeTrackerList.Clear();
        SetupRecipeTracker();
    }

    //TEMP RECIPE TRACKER [COMBINATION] - WHEN BUILDING THE 5 TOWERS
    #region Combination Temporary Recipe Tracker 
    public void UpdateTempRecipeTracker(Dictionary<GameObject, string> tempTower)
    {
        //UPGRADES

        //Check if there are any duplicates in the string 
        foreach (var twrBeingChecked in tempTower)
        {
            foreach (var twrBeingMatch in tempTower)
            {
                if (twrBeingChecked.Value == twrBeingMatch.Value && twrBeingChecked.Key != twrBeingMatch.Key)
                {
                    TowerObject twrObj = twrBeingChecked.Key.GetComponent<TowerObject>();

                    //Get next tier tower name
                    string towerName = twrObj.towerName;
                    string tierName = towerName.Substring(0, towerName.IndexOf(" "));
                    Tower.Tier nextTier = Tower.GetNextTier(tierName);
                    string upgTwrName = towerName.Replace(tierName, nextTier.ToString());

                    //Notify tower on upgraded ver
                    twrObj.AddTowerList(TowerObject.TowerListType.Upgradable, upgTwrName);

                }
            }
        }

        //COMBINATION 

        //Iterate through every recipe and insert tower built into the combination temp recipe tracker
        foreach (var recipe in tempRecipeTrackerList)
        {
            foreach (var twrBuilt in tempTower)
            {
                foreach (var twrReq in recipe.towerRequired.ToList())
                {
                    //twr built name == twrReq name
                    if (twrBuilt.Value == twrReq.Key && twrReq.Value == false)
                        InsertTempRecipeTrackerList(recipe.towerUpgrade, twrBuilt.Value);
                }

            }
        }
        //Check if there are combine available and notify tower
        CheckForTempCombine(tempTower);
    }

    private void CheckForTempCombine(Dictionary<GameObject, string> tempBuiltList)
    {
        //Iterate thru every recipe and check whether can be combined. If yes, notify the tower
        foreach (var recipe in tempRecipeTrackerList)
        {
            //Check If Twr Required is Met
            bool recipeCanCombine = true;
            foreach (var twrReq in recipe.towerRequired)
            {
                if (twrReq.Value == false)
                    recipeCanCombine = false;
            }

            if (recipeCanCombine)
            {
                foreach(var towerRequiredName in recipe.towerRequired)
                {
                    foreach(var towerBuilt in tempBuiltList)
                    {
                        if (towerRequiredName.Key == towerBuilt.Value)
                        {
                            towerBuilt.Key.GetComponent<TowerObject>().AddTowerList(TowerObject.TowerListType.Combinable, recipe.towerUpgrade);
                        }
                    }
                }
            }

        }
    }

    private void InsertTempRecipeTrackerList(string towerUpgrade, string towerBuilt)
    {
        //Find the recipe corresponding to the towerUpgrade and set the value
        foreach (var recipe in tempRecipeTrackerList)
        {
            if (recipe.towerUpgrade == towerUpgrade && recipe.towerRequired[towerBuilt] == false)
            {
                recipe.towerRequired[towerBuilt] = true;
            }
        }
    }
    #endregion
    //Utility
    #region Utility
    public void PrintTempRecipeTracker()
    {
        foreach (var recipe in tempRecipeTrackerList)
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
            PrintTempRecipeTracker();
        }
    }
    #endregion
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
    public Dictionary<string, bool> towerRequired;

    public RecipeTracker(string twrOutput, Dictionary<string, bool> twrRequired)
    {
        towerUpgrade = twrOutput;
        towerRequired = twrRequired;
    }
}
