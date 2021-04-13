using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Data_TowerPrefab", menuName = "ScriptableObject/Data_TowerPrefab")]

public class SO_TowerPrefab : ScriptableObject
{
    public TowerPrefab[] towerPrefabList;


    public GameObject GetGameObjectPrefab(string towerName)
    {
        TowerPrefab towerPrefab = Array.Find(towerPrefabList, element => element.towerName == towerName);
        if (towerPrefab == null)
        {
            Debug.LogWarning("Tower Named: " + towerName + " not found in SO_TowerPrefab");
            return null;
        }
        return towerPrefab.gameObject;
    }





    [System.Serializable]
    public class TowerPrefab
    {
        public string towerName;
        public GameObject gameObject;
    }
}
