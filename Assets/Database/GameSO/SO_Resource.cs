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

    public enum Stats
    {
        kingdomHealth, buildGem, gemChanceLevel, gold,
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
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ModifyStats(Stats stat, int amount)
    {
        switch (stat)
        {
            case Stats.kingdomHealth:
                kingdomHealth += amount;
                break;
            case Stats.buildGem:
                buildGem += amount;
                break;
            case Stats.gemChanceLevel:
                gemChanceLevel += amount;
                break;
            case Stats.gold:
                gold += amount;
                break;
            default:
                Debug.LogWarning("Stat : " + stat + " not found in enum");
                break;
        }
    }
    
}
