using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Tower", menuName = "ScriptableObject/TowerDB")]  
public class SO_Tower : ScriptableObject
{
#pragma warning disable CS0649

    [SerializeField] private List<Tower> towerDataList; //Do not ever rename this
#pragma warning restore CS0649

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
