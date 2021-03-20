using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    /// <summary>
    /// Enemy Object - Central information holder
    /// 
    /// -OnAwake : Get enemy info from database
    /// </summary>

    #region Variable Declaration
    public Enemy enemyInfo;
    private float enemyHP;
    ////////////////////////
    public string enemyName;
    public SO_Enemy enemyDB;
    public SO_GameEvent onEnemyKillEvent;
    [HideInInspector]
    #endregion

    #region Built-In Function
    private void Awake()
    {
        SetEnemyInfo();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            gameObject.SetActive(false);
            OnUnitKilled.enemyKilled(gameObject, gameObject);
        }
    }
    #endregion

    
    private void SetEnemyInfo()  //Sets enemy info based on database
    {
        enemyInfo = enemyDB.getEnemyInfo(enemyName);
        enemyHP = enemyInfo.maxHP;
        if (enemyInfo == null)
        {
            Debug.LogError("Enemy Named: " + enemyName + " not found in enemy DB");
        }
    }
    
    public void ReceiveDamage(float damage, GameObject attacker) //Takes damage and minus HP
    {
        enemyHP -= damage;
        Debug.Log("Current HP: " + enemyHP);

        //Checks if dead
        if (enemyHP <= 0)
        {
            OnUnitKilled.enemyKilled(gameObject, attacker);
        }
    }
}
