using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public SO_TowerRecipe twrRecipeSO;
    public Dictionary<GameObject, string> tempTwrBuiltList;
    private void Awake()
    {
        tempTwrBuiltList = new Dictionary<GameObject, string>();
    }
    private void Start()
    { 
        twrRecipeSO.SetupRecipeTracker();
    }
    private void OnEnable()
    {
        TowerObject.onBuildingInitialize += BuildingInitialize;
        SO_TrackerGamePhase.onPhaseBuild += PhaseBuild;
        SO_TrackerGamePhase.onPhaseSelectGem += PushTempTowerToDB;
    }


    private void OnDisable()
    {
        TowerObject.onBuildingInitialize -= BuildingInitialize;
        SO_TrackerGamePhase.onPhaseBuild -= PhaseBuild;
        SO_TrackerGamePhase.onPhaseSelectGem -= PushTempTowerToDB;

    }

    private void PhaseBuild()
    {
        tempTwrBuiltList.Clear();
        twrRecipeSO.ResetRecipeTracker();
    }
    private void PushTempTowerToDB()
    {
        twrRecipeSO.UpdateTempRecipeTracker(tempTwrBuiltList);
    }
    private void BuildingInitialize(string twrName, GameObject twrGO)
    {

        tempTwrBuiltList.Add(twrGO, twrName);

    }

}
