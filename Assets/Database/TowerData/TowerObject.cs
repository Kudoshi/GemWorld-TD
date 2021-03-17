using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    public string towerName;
    public Tower towerInfo;
    public SO_Tower towerDB;
    private void Awake()
    {
        SetTowerInfo();
    }

    private void SetTowerInfo()
    {
        towerInfo = towerDB.getTowerInfo(towerName);

        if (towerInfo == null)
        {
            Debug.LogError("Tower Named: " + towerName + " not found in tower DB");
        }
    }
}
