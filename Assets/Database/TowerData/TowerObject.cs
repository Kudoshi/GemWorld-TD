using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    public Tower towerInfo;
    public SO_Tower towerDB;
    private string towerName;

    private void Awake()
    {
        towerName = gameObject.name;
        towerName = towerName.Replace("(Clone)", "").Trim();
        Debug.Log(towerName);
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
