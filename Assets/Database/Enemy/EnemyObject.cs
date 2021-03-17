using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    public string enemyName;
    public Enemy enemyInfo;
    public SO_Enemy enemyDB;

    private void Awake()
    {
        SetEnemyInfo();
    }

    private void SetEnemyInfo()
    {
        enemyInfo = enemyDB.getEnemyInfo(enemyName);

        if (enemyInfo == null)
        {
            Debug.LogError("Enemy Named: " + enemyName + " not found in enemy DB");
        }
    }
}
