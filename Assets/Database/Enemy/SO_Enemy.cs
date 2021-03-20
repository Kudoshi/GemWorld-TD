using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Enemy", menuName = "ScriptableObject/EnemyDB")]
public class SO_Enemy : ScriptableObject
{
#pragma warning disable CS0649
    [SerializeField] private List<Enemy> enemyDataList;
#pragma warning restore CS0649

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
