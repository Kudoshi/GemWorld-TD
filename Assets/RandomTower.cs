using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RandomTower : MonoBehaviour
{
    public SO_Resource resourceSO;
    public SO_GemChance gemChanceSO;
    public SO_TowerPrefab towerPrefabSO;

    private int gemChanceLevel;
    private TierChance tierChance;
    private void Awake()
    {
        gemChanceLevel = resourceSO.gemChanceLevel;
        tierChance = Array.Find(gemChanceSO.GemTierChanceList, tierChanceArr => tierChanceArr.gemChanceLevel == gemChanceLevel);
    }
    private void Start()
    {
        RandomATower();
    }

    private void RandomATower()
    {
        //Generate percentage
        int chippedPerc = tierChance.chippedChance;
        int flawedPerc = chippedPerc + tierChance.flawedChance;
        int normalPerc = flawedPerc + tierChance.normalChance;
        int flawlessPerc = normalPerc + tierChance.flawlessChance;
        int perfectPerc = flawlessPerc + tierChance.perfectChance;

        //Roll
        int randomTierChance = UnityEngine.Random.Range(1, 101);
        if (randomTierChance <= chippedPerc)
            RollTower(Tower.Tier.Chipped);
        else if (randomTierChance <= flawedPerc)
            RollTower(Tower.Tier.Flawed);

        else if (randomTierChance <= normalPerc)
            RollTower(Tower.Tier.Normal);

        else if (randomTierChance <= flawlessPerc)
            RollTower(Tower.Tier.Flawless);

        else if (randomTierChance <= perfectPerc)
            RollTower(Tower.Tier.Perfect);

    }

    private void RollTower(Tower.Tier tier)
    {
        //Get Random Tower Type
        string towerType = GetRandomTower();

        Debug.Log(tier + " " + towerType);

        //Get tower prefab
        GameObject towerPrefab = towerPrefabSO.GetGameObjectPrefab(tier.ToString() + " " + towerType);

        Instantiate(towerPrefab, transform.position, towerPrefab.transform.rotation);
        Destroy(gameObject);

    }
    private string GetRandomTower()
    {
        int randomTowerNumber = UnityEngine.Random.Range(0, (System.Enum.GetValues(typeof(Tower.Type)).Length - 1));
        Debug.Log(randomTowerNumber);
        Tower.Type towerType = (Tower.Type)randomTowerNumber;

        return towerType.ToString();
    }
}
