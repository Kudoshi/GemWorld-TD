using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public SO_TowerRecipe twrRecipeSO;
    public Dictionary<GameObject, string> tempTwrBuiltList; //Stores the 5 tower built
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
        SO_TrackerGamePhase.onPhaseSelectGem += PhaseSelectGem;
    }
    private void OnDisable()
    {
        TowerObject.onBuildingInitialize -= BuildingInitialize;
        SO_TrackerGamePhase.onPhaseBuild -= PhaseBuild;
        SO_TrackerGamePhase.onPhaseSelectGem -= PhaseSelectGem;
    }
    private void BuildingInitialize(string twrName, GameObject twrGO)
    {
        //Add tower into tower built list
        tempTwrBuiltList.Add(twrGO, twrName);
    }
    private void PhaseBuild()
    {
        //Clear built list and SO's list
        tempTwrBuiltList.Clear();
        twrRecipeSO.ResetRecipeTracker();
    }
    private void PhaseSelectGem()
    {
        //Push list to SO for processing
        twrRecipeSO.UpdateTempRecipeTracker(tempTwrBuiltList);
        
    }
   

}
