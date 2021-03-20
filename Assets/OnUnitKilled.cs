using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OnUnitKilled : MonoBehaviour
{
    /// <summary>
    /// Function: Acts as a tool that raise OnUnitKilled events
    /// -Does not require to be instantiated to be used due to static function
    /// </summary>
    //                          Victim     Killer
    public static event Action<GameObject,GameObject> onEnemyKilled;
    public static void enemyKilled(GameObject victim, GameObject killer) //Kills victim and raises onUnitKilled event
    {
        if (DebugMode.debugUnitKilled)
        {
            Debug.Log("DESTROYED UNIT \n------------------------------------------------>  Victim: " + victim.name + " killed by : " + killer.name);
        }
        Destroy(victim);
        onEnemyKilled?.Invoke(victim, killer);
    }

}
