using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Enemy", menuName = "ScriptableObject/EnemyDB")]
public class SO_Enemy : ScriptableObject
{
    [SerializeField] private List<Enemy> enemyDataList;

    public Enemy getEnemyInfo(string enemyName)
    {
        foreach(var enemy in enemyDataList)
        {
            if (enemyName == enemy.enemyName)
            {
                return enemy;
            }
        }
        return null;
    }
}
