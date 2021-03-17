using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Tower", menuName = "ScriptableObject/TowerDB")]  
public class SO_Tower : ScriptableObject
{
    
    [SerializeField] private List<Tower> towerDataList; //Do not ever rename this

    public Tower getTowerInfo(string towerName)
    {
        foreach(var tower in towerDataList)
        {
            if (towerName == tower.towerName)
            {
                return tower;
            }
        }
        
        return null;
    }
}
