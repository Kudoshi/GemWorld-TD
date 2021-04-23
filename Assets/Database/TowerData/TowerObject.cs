using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Not implemented: Ability to add kill count
/// </summary>
public class TowerObject : MonoBehaviour
{
    public SO_Tower towerDB;
    
    //Set-able tower info
    public string towerName { get; private set; }
    public Tower towerInfo { get; private set; }
    public int killCount { get; private set; }
    [HideInInspector] public bool canDowngrade { get; private set; }

    //Tower event
    public event Action<TowerObject> onStatChange;
    public static event Action<string, GameObject> onBuildingInitialize;
    public static event Action<string, GameObject> onFinishBuilding;
    
    //View-only List
    [Header("View Only [Do not touch]")]
    public List<string> upgradableTowerList;
    public List<string> combinableTowerList;

    private void Awake()
    {
        //Set tower name

        SetTowerUp();
        
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

    private void SetTowerUp()
    {
        //Setup tower name
        towerName = gameObject.name;
        towerName = towerName.Replace("(Clone)", "").Trim();

        //Copy towerInfo
        Tower tempTowerInfo = new Tower();
        tempTowerInfo = towerDB.getTowerInfo(towerName);
        towerInfo = tempTowerInfo;

        if (towerInfo == null)
        {
            Debug.LogError("Tower Named: " + towerName + " not found in tower DB");
        }

        //Check if can downgrade
        if (towerInfo.tier == Tower.Tier.Chipped)
        {
            canDowngrade = false;
        }
        else
        {
            Debug.Log(towerName + "can downgrade coz it is : " + towerInfo.tier);
            canDowngrade = true;
        }
    }
    public void AddTowerList(TowerListType twrListType, string twrOutput)
    {
        if (twrListType == TowerListType.Combinable)
        {
            if(!combinableTowerList.Contains(twrOutput))
            {
                Debug.Log("== Can COMBINE tower: " + towerName + " into : " + twrOutput);
                combinableTowerList.Add(twrOutput);

            }
        }
        else if (twrListType == TowerListType.Upgradable)
        {
            if (!upgradableTowerList.Contains(twrOutput))
            {
                Debug.Log("== Can upgrade tower: " + towerName + " into : " + twrOutput);
                upgradableTowerList.Add(twrOutput);
            }
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
    public enum TowerListType
    {
        Upgradable, Combinable
    }
    private void CheckKillCount()
    {
        throw new NotImplementedException();
    }
}
