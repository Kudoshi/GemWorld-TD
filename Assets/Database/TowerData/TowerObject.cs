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
    public static event Action<string, GameObject> onBuildingInitialize;
    public static event Action<string, GameObject> onFinishBuilding;
    public string towerName;
    public int killCount;

    public List<string> upgradableTowerList;
    public List<string> combinableTowerList;

    private void Awake()
    {
        //Set tower name
        towerName = gameObject.name;
        towerName = towerName.Replace("(Clone)", "").Trim();
        SetTowerInfo();
        
    }
    private void Start()
    {
        BuildingInitialize();
        StartBuildingAnimation();
    }
    private void StartBuildingAnimation()
    {
        //Play building animation
        //Onfinish building animation
        Invoke("FinishBuildingAnimation", 1f);
    }
    private void FinishBuildingAnimation()
    {
        onFinishBuilding?.Invoke(towerName, gameObject);
    }
    private void BuildingInitialize()
    {
        onBuildingInitialize?.Invoke(towerName, gameObject);
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
    public void AddTowerList(TowerListType twrListType, string twrOutput)
    {
        Debug.Log("== Can upgrade tower: " + towerName +" into : " + twrOutput);
        if (twrListType == TowerListType.Combinable)
            combinableTowerList.Add(twrOutput);
        else if (twrListType == TowerListType.Upgradable)
            upgradableTowerList.Add(twrOutput);
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
    public enum TowerListType
    {
        Upgradable, Combinable
    }
    private void CheckKillCount()
    {
        throw new NotImplementedException();
    }
}
