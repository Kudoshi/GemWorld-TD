using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Not implemented: Ability to add kill count
/// </summary>
public class TowerObject : MonoBehaviour
{
    public Tower towerInfo;
    public SO_Tower towerDB;

    public event Action<TowerObject> onStatChange; 

    public string towerName;
    public int killCount;

    private void Awake()
    {
        towerName = gameObject.name;
        towerName = towerName.Replace("(Clone)", "").Trim();
        Debug.Log(towerName);
        SetTowerInfo();
    }

    private void SetTowerInfo()
    {
        //Copy towerInfo
        Tower tempTowerInfo = new Tower();
        tempTowerInfo = towerDB.getTowerInfo(towerName);
        towerInfo = tempTowerInfo;

        if (towerInfo == null)
        {
            Debug.LogError("Tower Named: " + towerName + " not found in tower DB");
        }
    }

    public void ModifyStats(string statName, int amount)
    {
        switch (statName)
        {
            case "atkDamage":
                towerInfo.atkDamage += amount;
                break;
            case "atkProjectileSpeed":
                towerInfo.atkProjectileSpeed += amount;
                break;
            case "atkRange":
                towerInfo.atkRange += amount;
                break;
            case "atkSpeed":
                towerInfo.atkSpeed += amount;
                break;
            case "killCount":
                killCount += amount;
                CheckKillCount();
                break;
            default:
                Debug.LogWarning(statName + " not found in towerInfo to modify");
                return;
                
        }

        onStatChange?.Invoke(this);
    }

    private void CheckKillCount()
    {
        throw new NotImplementedException();
    }
}
