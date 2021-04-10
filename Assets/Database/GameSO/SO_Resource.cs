using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Tracker_Resource", menuName = "ScriptableObject/Tracker_Resource")]
public class SO_Resource : ScriptableObject
{
    private int kingdomMaxHealth =100;
    private int maxBuildGem = 5;
    public int kingdomHealth;
    public int buildGem;
    public int gemChanceLevel;
    public int gold;

    public void ResetResource()
    {
        kingdomHealth = kingdomMaxHealth;
        buildGem = 0;
        gemChanceLevel = 1;
        gold = 1;
    }

    public void AddGold(int gold)
    {
        this.gold += gold;
    }
    public void AddGemDefault()
    {
        buildGem = maxBuildGem;
    }
    public bool CheckCanBuild()
    {
        //check if have resource
        if (buildGem > 0)
        {
            buildGem--;
            return true;
        }
        else if (buildGem == 0)
        {
            Debug.LogWarning("Should not be able to access this part");
            return false;
        }
        else
        {
            Debug.LogWarning("Should not be able to access this part");
            return false;
        }
    }

    
}
